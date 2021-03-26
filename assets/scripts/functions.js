function OnDataBound(sender, args) {
    sender.expand(sender.get_items()[1])
}

function timeConvert(millis) {
    let minAux = millis / 1000 / 60
    let hours = minAux / 60
    let hoursFloor = Math.floor(hours)
    let min = (hours - hoursFloor) * 60

    return min < 1 ? `${(min * 60).toFixed(3)} segundos` : `${hoursFloor}h ${min.toFixed(0)}min`
}