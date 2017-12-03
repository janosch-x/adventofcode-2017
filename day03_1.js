class Element {
  constructor({number = 1, x = 0, y = 0, aim = 'right'}) {
    this.number = number
    this.x = x
    this.y = y
    this.aim = aim

    this.level = 0
    for (let unplaced = number - 1; unplaced > 0; this.level++) {
      unplaced = unplaced - ((this.level + 1) * 8)
    }
  }

  static number(number) {
    let el = new Element({})
    while (el.number < number) el = el.next()
    return el
  }

  next() {
    const next = new Element({...this, number: this.number + 1})
    if (this.hitsBounds && next.level == this.level) next.turn()
    next.move()
    return next
  }

  get hitsBounds() {
    const advance = ['left', 'right'].includes(this.aim) ? this.x : this.y
    return Math.abs(advance) >= this.level
  }

  turn() {
    const aims = ['right', 'up', 'left', 'down']
    this.aim = aims[aims.indexOf(this.aim) + 1] || aims[0]
  }

  move() {
    if      (this.aim == 'right') this.x++
    else if (this.aim == 'up')    this.y++
    else if (this.aim == 'left')  this.x--
    else if (this.aim == 'down')  this.y--
  }

  get manhattanDistanceFromCenter() {
    return Math.abs(this.x) + Math.abs(this.y)
  }
}

console.log('result:', Element.number(265149).manhattanDistanceFromCenter)
