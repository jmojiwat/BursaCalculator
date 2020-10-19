namespace Infrastructure

[<Struct; StructuralEquality; StructuralComparison>]
type Lot =
    val value: int
    new(value) = { value = value }
    static member (+) (left: Lot, right: Lot) = left.value + right.value |> Lot
    static member (-) (left: Lot, right: Lot) = left.value - right.value |> Lot

    static member (*) (left: Lot, right) = left.value * right * 100
    static member (*) (left, right: Lot) = left * right.value * 100

    static member int(source: Lot): int = source.value
        

[<Struct; StructuralEquality; StructuralComparison>]
type Percent =
    val value: decimal
    new(value) = { value = value }
    static member (+)(left: Percent, right: Percent) = left.value + right.value |> Percent
    static member (-)(left: Percent, right: Percent) = left.value - right.value |> Percent

    static member (*)(left: Percent, right) = left.value * right / 100m |> Percent
    static member (*)(left, right: Percent) = left * right.value / 100m |> Percent

    static member (/)(left: Percent, right) = left.value / right * 100m |> Percent
    static member (/)(left, right: Percent) = left / right.value * 100m |> Percent

    static member decimal(source: Percent): decimal = source.value

[<Struct; StructuralEquality; StructuralComparison>]
type Share =
    val value: int
    new(value) = { value = value }
    static member (/)(left: Share, right) = left.value / right

    static member int(source: Share): int = source.value

[<Struct; StructuralEquality; StructuralComparison>]
type Tick =
    val value: int
    new(value) = { value = value }
    static member int(source: Tick): int = source.value

[<Struct; StructuralEquality; StructuralComparison>]
type Money = 
    val value: decimal
    new(value) = { value = value }
    static member (+) (left: Money, right: Money) = left.value + right.value |> Money
    static member (-) (left: Money, right: Money) = left.value - right.value |> Money
    static member (/) (left: Money, right: Money) = left.value / right.value |> Money
    static member (*) (left: Money, right: Percent) = left.value * Percent.decimal(right) / 100m |> Money
    static member (*) (left: Percent, right: Money) = (left |> Percent.decimal) * right.value / 100m |> Money
    static member (*) (left: Money, right: Lot) = left.value * (right |> Lot.int |> decimal) * 100m |> Money
    static member (*) (left: Lot, right: Money) = (left |> Lot.int |> decimal) * right.value * 100m |> Money
    static member (*) (left: Money, right: Tick) = left.value * (right |> Tick.int |> decimal) |> Money
    static member (*) (left: Tick, right: Money) = (left |> Tick.int |> decimal) * right.value |> Money
    static member (/) (left: Money, right: Lot) = left.value / (right |> Lot.int |> decimal) * 100m |> Money
    static member (/) (left: Money, right) = left.value / right |> Money
    static member decimal(source: Money): decimal = source.value

module InfrastructureExtensions = 
    let toTick fromPrice toPrice =
        let tickSize = min fromPrice toPrice
        ((fromPrice - toPrice) / tickSize) |> Money.decimal |> int |> Tick

    let Money amount = 
            new Money(amount)

    let ToTickSize price = 
        match price with
        | price when price < (1m |> Money) -> 0.005m |> Money
        | price when price < (9.99m |> Money) -> 0.01m |> Money
        | price when price < (99.99m |> Money) -> 0.02m |> Money
        | _ -> 1m |> Money


    let Lot lots = 
        new Lot(lots)

    let ToShare lots = 
        new Share (lots * 100)
    
    let Percent percent = 
        new Percent(percent)

    let ToPercent value = 
        new Percent(value * 100m)

    let Share share = 
        new Share(share)

    let ToLot shares = 
        new Lot(shares / 100)

    let Tick ticks = 
        new Tick(ticks)



