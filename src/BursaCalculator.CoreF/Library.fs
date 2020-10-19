namespace BursaCalculator.CoreF

module PositionCalculatorExtensions =
    open System
    open Infrastructure
    open Infrastructure.InfrastructureExtensions

    let accountRisk capital risk =
        capital * risk

//private static Option<Share> SharesToLong(Money entryPrice, Money stopLossPrice, Money accountRisk) =>
//Try(() => (int) (accountRisk / (entryPrice - stopLossPrice)))
//    .Map(Share)
//    .ToOption();

    let sharesToLong entryPrice stopLossPrice accountRisk =
        try
            (accountRisk / (entryPrice - stopLossPrice)) |> Money.decimal |> int
            |> Share |> Some
        with
        | :? DivideByZeroException -> None

    let shares entryPrice stopLossPrice accountRisk =
        sharesToLong entryPrice stopLossPrice accountRisk

    let lots entryPrice stopLossPrice accountRisk =
        sharesToLong entryPrice stopLossPrice accountRisk
            |> ToLot

    let stopLossAmount entryPrice stopLossPrice lots =
        (entryPrice - stopLossPrice) * lots

    let stopLossPercentage entryPrice stopLossPrice =
        try
            (entryPrice - stopLossPrice) / entryPrice |> ToPercent |> Some
        with
        | :? DivideByZeroException -> None

    let stopLossTick entryPrice stopLossPrice =
        toTick entryPrice stopLossPrice

    let targetAmount entryPrice targetPrice lots =
        (targetPrice - entryPrice) * lots

    let targetPercentage entryPrice targetPrice =
        try
            (targetPrice - entryPrice) / entryPrice |> ToPercent |> Some
        with
        | :? DivideByZeroException -> None

    let targetTick entryPrice targetPrice =
        toTick targetPrice entryPrice

    let riskRewardLong entryPrice stopLossPrice targetPrice =
        let dividend = (targetPrice - entryPrice)
        let divisor = (entryPrice - stopLossPrice)

        if(dividend < 0m || divisor < 0m) then
            None
        else
            try
                Some(dividend / divisor)
            with
            | :? DivideByZeroException -> None

    let riskReward entryPrice stopLossPrice targetPrice =
        riskRewardLong entryPrice stopLossPrice targetPrice

    let entryAmount entryPrice lots =
        entryPrice * lots
