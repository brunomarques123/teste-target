# Teste Target

Solução dos 3 exercícios do desafio: **Angular 22 + C# (.NET 8) + SQL Server**.
Cada exercício tem uma tela no front (`/comissoes`, `/estoque`, `/juros`) e um endpoint na API REST.

## Como rodar

**Pré-requisitos:** .NET 8 SDK, Node 24+, Angular CLI 22, SQL Server (local) e a ferramenta `dotnet-ef`.

1. **Connection string** — em `backend/src/TesteTarget.Api/appsettings.json`. O padrão usa a instância padrão do SQL Server local com autenticação do Windows:
   `Server=localhost;Database=TesteTarget;Trusted_Connection=True;TrustServerCertificate=True`.
   Para SQL Server Express, use `Server=localhost\SQLEXPRESS`; para LocalDB, `Server=(localdb)\MSSQLLocalDB`.
2. **API** (cria o banco `TesteTarget` e carrega o seed automaticamente em Development):
   ```bash
   cd backend/src/TesteTarget.Api
   dotnet run --launch-profile http      # http://localhost:5086  (Swagger em /swagger)
   ```
3. **Front:**
   ```bash
   cd frontend
   npm install
   ng serve                              # http://localhost:4200
   ```
4. **Testes** (regra de comissão): `cd backend && dotnet test`

Para criar o banco manualmente em vez de no startup:
`dotnet ef database update -p src/TesteTarget.Infrastructure -s src/TesteTarget.Api`

## Arquitetura

```
backend/src
├─ TesteTarget.Domain          regras de negócio puras, sem dependências
│    CalculadoraComissao · CalculadoraJuros · Produto.Movimentar()
├─ TesteTarget.Application     IService/Service, DTOs e as interfaces dos repositories (IVendaRepository, IEstoqueRepository)
├─ TesteTarget.Infrastructure  EF Core: DbContext, Repository, migrations e seed
└─ TesteTarget.Api             Controllers finos, middleware de erros, Swagger, CORS
```

Fluxo: `Controller → IService/Service → (Domain) → IRepository/Repository → SQL Server`.
As dependências apontam para dentro: `Application` não conhece o EF Core.
Comissões e estoque usam banco e repository (vendas e movimentações ficam no SQL Server); o juros é cálculo puro e não precisa de repository (Controller → Service → Domain).

## Os exercícios e as premissas adotadas

### 1. Comissão — `GET /api/comissoes`
- Venda `< R$ 100,00` → 0% · `R$ 100,00 a R$ 499,99` → 1% · `≥ R$ 500,00` → 5%.
- Premissa: "a partir de R$ 500,00" **inclui** R$ 500,00 exato (há uma venda de Carlos nesse valor).
- A comissão é calculada **por venda** (valor exato) e somada por vendedor; o arredondamento a 2 casas (`MidpointRounding.AwayFromZero`) é feito **uma única vez, no total do vendedor**, para não acumular erro de centavos. Uso `decimal`, nunca `double`.
- As vendas do JSON do teste estão na tabela `Venda` (seed da migration). A API lê do SQL Server, calcula e devolve o resultado; a tela só exibe.

### 2. Estoque — `POST /api/estoque/movimentacoes`
- Tabelas `Produto` (saldo) e `Movimentacao` (id único *identity*, tipo, descrição, quantidade, saldo após, data). O seed carrega os produtos do JSON.
- Cada movimentação tem **tipo** (Entrada/Saída) e **descrição** livre (ex.: "Compra de fornecedor").
- A resposta traz o **saldo final** do produto movimentado.
- Saída maior que o saldo, quantidade ≤ 0 e produto inexistente são rejeitados (HTTP 422 com mensagem clara).
- Saldo e movimentação são gravados no mesmo `SaveChanges` (transação). `RowVersion` no produto evita sobrescrita em movimentações simultâneas.

### 3. Juros — `POST /api/juros/calcular`
- O enunciado diz "multa de 2,5% ao dia" e pede "juros". Premissa: **juros simples** = `valor × 2,5% × dias de atraso`.
- Dias de atraso = hoje − vencimento (mínimo 0; título a vencer não gera juros).
- A taxa é configurável em `appsettings.json` (`Juros:TaxaDiaria`). "Hoje" vem de um `TimeProvider` injetado, o que facilita testar.

## Decisões
- **Testes:** cobrem a regra de comissão (a que tem mais casos de borda: R$ 99,99 / 100 / 499,99 / 500). Estoque e juros foram validados manualmente.
- **Erros:** um middleware converte `RegraDeNegocioException` em `ProblemDetails` (422), então os controllers não têm `try/catch`.
- **Fora do escopo (de propósito):** autenticação, juros compostos, tela para cadastrar vendas (elas vêm do seed).
