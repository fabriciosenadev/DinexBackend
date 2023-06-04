namespace Dinex.Core;

public enum InvestmentActivityType
{
    [Description("Bonificação de Ativos")]
    AssetBonus,

    [Description("Cessão de Direitos")]
    AssignmentOfRights,

    [Description("Cessão de Direitos - Solicitada")]
    AssignmentOfRightsRequested,

    [Description("Compra")]
    Buy,

    [Description("Dividendo")]
    Dividend,

    [Description("Leilão de Fração")]
    FractionAuction,

    [Description("Fração de Ativos")]
    FractionOfAssets,

    [Description("Juros Sobre Capital Proprio")]
    InterestOnEquity,

    [Description("Direito de Subscrição")]
    SubscriptionRight,

    [Description("Direito de Subscrição - Não Exercido")]
    SubscriptionRightNotExercised,

    [Description("Transferencia - Liquidação")]
    SettlementTransfer,

    [Description("Transferencia")]
    Transfer,

    [Description("Rendimento")]
    Yield,
    
    [Description("Rendimento - Transferido")]
    YieldTransferred,
}
