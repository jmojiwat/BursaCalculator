namespace BursaCalculator.CoreF

open System
open Infrastructure

module PositionCalculatorExtensions =

    let accountRisk capital risk =
        capital * risk

    let sharesToLong entryPrice stopLossPrice accountRisk =
        try
            (accountRisk / (entryPrice - stopLossPrice)) |> Money.decimal |> int
            |> share |> Some
        with
        | :? DivideByZeroException -> None

    let shares entryPrice stopLossPrice accountRisk =
        sharesToLong entryPrice stopLossPrice accountRisk

    let lots entryPrice stopLossPrice accountRisk =
        sharesToLong entryPrice stopLossPrice accountRisk
        |> Option.map (fun s -> s |> Share.int |> toLot)

    let stopLossAmount entryPrice stopLossPrice lots =
        (entryPrice - stopLossPrice) * lots

    let stopLossPercentage entryPrice stopLossPrice =
        try
            (entryPrice - stopLossPrice) / entryPrice |> toPercent |> Some
        with
        | :? DivideByZeroException -> None

    let stopLossTick entryPrice stopLossPrice =
        toTick entryPrice stopLossPrice

    let targetAmount entryPrice targetPrice lots =
        (targetPrice - entryPrice) * lots

    let targetPercentage entryPrice targetPrice =
        try
            (targetPrice - entryPrice) / entryPrice |> toPercent |> Some
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

    let isValidStopLossPrice entryPrice stopLossPrice =
        entryPrice > stopLossPrice

    let isValidTargetPrice entryPrice targetPrice =
        entryPrice < targetPrice

module PositionAltCalculatorExtensions =
    let risk (capital: Money, accountRisk: Money) =
        try
            accountRisk / capital |> Some
        with
        | :? DivideByZeroException -> None

    let stopLossPrice (accountRisk: Money, entryPrice: Money, lots: Lot) =
        try
        (lots * entryPrice - accountRisk) / lots |> Some
        with
        | :? DivideByZeroException -> None

    let stopLossPriceFromPercent (entryPrice:Money, stopLossPecent:Percent) =
        entryPrice - entryPrice * stopLossPecent

    let stopLossPriceFromTick (entryPrice, stopLossTick: Tick) =
        entryPrice - stopLossTick * (ToTickSize entryPrice)

    let targetPriceFromPercent (entryPrice: Money, targetPercent: Percent) =
        targetPercent * entryPrice + entryPrice

    let targetPriceFromTick (entryPrice, targetTick: Tick) =
        targetTick * (ToTickSize entryPrice) + entryPrice
