serial.writeLine("HE")//@BB[HE] Hello World!
serial.writeLine("Ss")//@BB[Ss] Starting animation.

basic.forever(() => {
    serial.writeLine("TO")//@BB[TO] Toggling LEDs.
    led.toggleAll();

    serial.writeLine("AR")//@BB[AR] Reading Accelerometer...
    let x: number = input.acceleration(Dimension.X)
    let y: number = input.acceleration(Dimension.Y)
    let z: number = input.acceleration(Dimension.Z)

    //@BB[AV] Accelerometer Values: X={0} Y={1} Z={2}
    serial.writeLine("AV" + x + " " + y + " " + z)
})

input.onGesture(Gesture.ScreenDown, () => {
    serial.writeLine("GE2")//@BB[GE] Gesture.{0:(0)Shake,(1)ScreenUp,(2)ScreenDown}
})

input.onGesture(Gesture.ScreenUp, () => {
    serial.writeLine("GE1")//@BB[GE] Gesture.{0:(0)Shake,(1)ScreenUp,(2)ScreenDown}
})

input.onGesture(Gesture.Shake, () => {
    serial.writeLine("GE0")//@BB[GE] Gesture.{0:(0)Shake,(1)ScreenUp,(2)ScreenDown}
})
