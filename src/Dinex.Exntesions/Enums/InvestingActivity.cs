﻿namespace Dinex.Core;

public enum InvestingActivity
{
    [Description("Bonificação em Ativos")]
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

    [Description("Fração em Ativos")]
    FractionOfAssets,

    [Description("Juros Sobre Capital Próprio")]
    InterestOnEquity,

    [Description("Direito de Subscrição")]
    SubscriptionRight,

    [Description("Direitos de Subscrição - Não Exercido")]
    SubscriptionRightsNotExercised,

    [Description("Transferência - Liquidação")]
    SettlementTransfer,

    [Description("Transferência")]
    Transfer,

    [Description("Rendimento")]
    Yield,
    
    [Description("Rendimento - Transferido")]
    YieldTransferred,
}
