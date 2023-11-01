//#region values
var tableOfFields
var winningLine = []
var playerSigns = []
var currentPlayerSign = ""
var currentPlayer = 0
var Points = []
//#endregion

function Run() {
    document.getElementById("board").innerHTML = ""
    let tableSize = document.getElementById("number").value
    tableOfFields = ""
    tableOfFields = CreateTableOfFields(tableSize)
    currentPlayerSign = ""
    currentPlayer = 0
    playerSigns = []
    winningLine = []
    playerSigns = GetSignsOfPlayers()
    CreateBoard(tableSize)
}
function CreateBoard(tableSize) {
    let div = document.getElementById("board")
    CreateTable(div, tableSize)
}
function CreateTable(div, tableSize) {
    let table = document.createElement("table")
    div.appendChild(table)
    for (let i = 0; i < tableSize; i++) {
        CreateRow(tableSize, table, i)
    }
}
function CreateRow(tableSize, table, i) {
    let row = document.createElement("tr")
    table.appendChild(row)
    for (let j = 0; j < tableSize; j++) {
        CreateField(tableSize, row, i, j)
    }
}
function CreateField(tableSize, row, i, j) {
    row.appendChild(CreateButtons(i, j, tableSize))
}
function CreateButtons(i, j, tableSize) {
    let button = document.createElement("button")
    button.innerText = ""
    button.addEventListener("click", () => {
        Gameplay(i, j, tableSize)
    })
    tableOfFields[i][j] = button
    return button
}
function CreateTableOfFields(tableSize) {
    var tableOfFields = []
    for (let i = 0; i < tableSize; i++) {
        tableOfFields[i] = [tableSize]
    }
    return tableOfFields
}
function GetSignsOfPlayers() {
    let tmpPlayersigns = document.getElementById("players").value
    if (tmpPlayersigns == "" || tmpPlayersigns == "-") {
        alert("Check your signs \nyou cannot use '-' or ''")
        location.reload()
    }
    for (let i = 0; i < tmpPlayersigns.length; i++) {
        if (tmpPlayersigns[i] != " ") {
            playerSigns.push(tmpPlayersigns[i])
            console.log(playerSigns[i])
        }
    }
    return playerSigns
}
function Gameplay(row, col, tableSize) {
    currentPlayerSign = playerSigns[currentPlayer]

    if (CheckIfFieldIsNotAttached(row, col)) {
        insertSignsToBoard(row, col, currentPlayerSign)
    }
    else {
        currentPlayer--
    }
    if (CheckWins(currentPlayerSign, tableSize)) {
        SetClassToWinningLine(winningLine)
        setTimeout(() => {
            WinningCall(currentPlayerSign)
        }, 500)
    }
    else if (CheckForDraw(tableSize)) {
        setTimeout(() => {
            alert("draw in game")
            location.reload()
        }, 1000);
    }
    currentPlayer++
    if (currentPlayer > playerSigns.length - 1) {
        currentPlayer = 0
    }
}
function CheckIfFieldIsNotAttached(row, col) {
    if (tableOfFields[row][col].innerText != "") {
        alert("already occupied\nselect other field")
        return false
    }
    return true
}
function insertSignsToBoard(row, col, currentPlayerSign) {
    tableOfFields[row][col].innerText = currentPlayerSign
}
function CheckWins(player, tableSize) {
    let isTrue = false
    if (CheckRow(tableSize, player)) {
        isTrue = true
    }
    if (CheckCollumn(tableSize, player)) {
        isTrue = true
    }
    if (CheckCornerToCorner(tableSize, player)) {
        isTrue = true
    }
    return isTrue
}
function CheckRow(tableSize, player) {
    for (let i = 0; i < tableSize; i++) {
        let arrayOfFields = []
        for (let j = 0; j < tableSize; j++) {
            arrayOfFields.push(tableOfFields[i][j])
        }
        if (CheckLine(arrayOfFields, tableSize, player)) {
            return true
        }
    }
    return false
}
function CheckCollumn(tableSize, player) {
    for (let i = 0; i < tableSize; i++) {
        let arrayOfFields = []
        for (let j = 0; j < tableSize; j++) {
            arrayOfFields.push(tableOfFields[j][i])
        }
        if (CheckLine(arrayOfFields, tableSize, player)) {
            return true
        }
    }
    return false
}
function CheckCornerToCorner(tableSize, player) {
    let isTrue = false
    if (CheckX(tableSize, player))
        isTrue = true
    if (CheckY(tableSize, player))
        isTrue = true
    return isTrue
}
function CheckY(tableSize, player) {
    let arrayOfFields = []
    for (let j = 0; j < tableSize; j++) {
        arrayOfFields.push(tableOfFields[j][j])
    }
    if (CheckLine(arrayOfFields, tableSize, player)) {
        return true
    }
}
function CheckX(tableSize, player) {
    let tmp = tableSize - 1
    arrayOfFields = []
    for (let i = 0; i < tableSize; i++) {
        arrayOfFields.push(tableOfFields[tmp--][i])
    }
    if (CheckLine(arrayOfFields, tableSize, player)) {
        return true
    }
    return false
}
function CheckLine(arrayOfFields, tableSize, player) {
    for (let i = 0; i < tableSize; i++) {
        if (arrayOfFields[i].innerText != player) {
            return null
        }
    }
    winningLine.push(arrayOfFields)
    return true
}
function SetClassToWinningLine(arrayOfFields) {
    arrayOfFields.forEach(element => {
        element.forEach(cell => {
            cell.className = "winningLine"
        });
    });
}
function WinningCall(playerSign) {
    alert(playerSign + " wins!")
    location.reload()
    //Run()
}
function CheckForDraw(tableSize) {
    for (let i = 0; i < tableSize; i++) {
        for (let j = 0; j < tableSize; j++) {
            if (tableOfFields[i][j].innerText == "") {
                return false
            }
        }
    }
    return true
}