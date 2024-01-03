fpsInterval = 1000 / 30
then = Date.now();
const pixelSize = 10;
const r = Math.trunc(window.innerHeight / pixelSize);
const c = Math.trunc(window.innerWidth / pixelSize);
const alive = 0.5;
let grid = [];
const trueColor = "White";
const falseColor = "#181818";


function getConvalution(x, y){
    let sLiveCount = 0;
    for (var i = -1; i <= 1; i++)
    {
        for (var j = -1; j <= 1; j++)
        {
            if (i == 0 && j == 0) continue;
            sLiveCount += grid[(x + i + r) % r][(y + j + c) % c] ? 1 : 0;
        }
    }
    if (grid[x][y]) {
        if (sLiveCount < 2) return false;
        if (sLiveCount < 4) return true;
        return false;
    }
    else {
        if (sLiveCount == 3) return true;
        return false;
    }
}

function update(){
    let newGrid = [];
    for(var i = 0; i < r; i++) {
        let rEle = []
        for (var j = 0; j < c; j++) {
            rEle.push(getConvalution(i,j))
        }
        newGrid.push(rEle);
    }
    return newGrid;
}

function render(gridele){
    for (var i = 0; i < r; i++) {
        for (var j = 0; j < c; j++) {
            document.getElementById(i + " " + j).style.backgroundColor = grid[i][j] ? trueColor : falseColor;
        }
    }
}

(() => {
    for (var i = 0; i < r; i++) {
        let rEle = []
        for (var j = 0; j < c; j++) {
            rEle.push(Math.random() < alive)
        }
        grid.push(rEle);
    }

    console.table(grid);    
    let gridele = document.getElementById("gol");

    for (var i = 0; i < r; i++) {
        let node1 = document.createElement("div")
        node1.className = "flex_row"
        for (var j = 0; j < c; j++) {
            let node2 = document.createElement("div")
            node2.style.backgroundColor = grid[i][j] ? trueColor : falseColor;
            node2.style.width = pixelSize + "px";
            node2.style.height = pixelSize + "px";
            node2.id = i + " " + j;
            node1.appendChild(node2);
        }
        gridele.appendChild(node1);
    }

    function loop() {

        now = Date.now();
        elapsed = now - then;

        if (elapsed > fpsInterval) {

            then = now - (elapsed % fpsInterval);

            grid = update();
            render(gridele);
        }

        requestAnimationFrame(loop);
    }
    requestAnimationFrame(loop)
})()