namespace BursaCalculator.CoreF

module Infrastructure =
    [<Struct; StructuralEquality; StructuralComparison; StructuredFormatDisplay("{value} MYR/share")>]
    type PerQuantity =
        val internal value: decimal
        new(value) = { value = value }
        member this.perShare with get () = this.value
        member this.perLot with get () = this.value / 100m

    [<Struct; StructuralEquality; StructuralComparison; StructuredFormatDisplay("{value} shares")>]
    type Quantity =
        val internal value: int
        new(value) = { value = value }
        static member (+) (left: Quantity, right: Quantity) = left.value + right.value |> Quantity
        static member (-) (left: Quantity, right: Quantity) = left.value - right.value |> Quantity
        static member (/) (left: Quantity, right: Quantity) = left.value / right.value

        static member (*) (left: Quantity, right) = left.value * right |> Quantity
        static member (*) (left, right: Quantity) = left * right.value |> Quantity
    
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
    type Tick =
        val value: int
        new(value) = { value = value }
        static member int(source: Tick): int = source.value

    [<Struct; StructuralEquality; StructuralComparison>]
    type Money = 
        val internal value: decimal
        new(value) = { value = value }
        static member (+) (left: Money, right: Money) = left.value + right.value |> Money
        static member (-) (left: Money, right: Money) = left.value - right.value |> Money
        static member (/) (left: Money, right: Money) = left.value / right.value |> Money
        static member (*) (left: Money, right: Percent) = left.value * Percent.decimal(right) / 100m |> Money
        static member (*) (left: Percent, right: Money) = (left |> Percent.decimal) * right.value / 100m |> Money
        static member (*) (left: Money, right: Quantity) = left.value * (right.value |> decimal) |> Money
        static member (*) (left: Quantity, right: Money) = (left.value |> decimal) * right.value |> Money
        static member (*) (left: Money, right: Tick) = left.value * (right |> Tick.int |> decimal) |> Money
        static member (*) (left: Tick, right: Money) = (left |> Tick.int |> decimal) * right.value |> Money
        static member (/) (left: Money, right: Quantity) = left.value / (right.value |> decimal) |> PerQuantity
        static member (/) (left: Money, right) = left.value / right |> Money
        static member decimal(source: Money): decimal = source.value

    let shares amount = amount |> Quantity
    let lots amount = amount * 100 |> Quantity

    let s = shares 1
    let l = lots 1

    let toTick fromPrice toPrice =
        let tickSize = min fromPrice toPrice
        ((fromPrice - toPrice) / tickSize) |> Money.decimal |> int |> Tick

    let money amount = 
            new Money(amount)

    let ToTickSize price = 
        match price with
        | price when price < (1m |> Money) -> 0.005m |> Money
        | price when price < (9.99m |> Money) -> 0.01m |> Money
        | price when price < (99.99m |> Money) -> 0.02m |> Money
        | _ -> 1m |> Money


    let percent percent = 
        new Percent(percent)

    let toPercent value = 
        new Percent(value * 100m)

    let tick ticks = 
        new Tick(ticks)



