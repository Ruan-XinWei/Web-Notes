var slider = document.getElementsByClassName("slider")[0]
var text = document.getElementsByClassName("text")[0]
var button = document.getElementsByClassName("button")[0]
var section = document.getElementsByClassName("section")[0]
var background = document.getElementsByClassName("background")[0]
var icon = document.getElementById("icon")
var success_section = document.getElementsByClassName("success_section")[0]

var max_length = slider.offsetWidth - button.offsetWidth    //最大移动长度
var half = max_length / 2   //半长
var success = false //默认不成功
var start_button_position = 0   //初始化开始位置
var move_length = 0 //初始化移动位置
var length = function () {
    return Math.ceil(Math.random() * half + half)
}
var success_length = length() //获取随机长度

var success_position_x = success_length + 2 //x轴
var success_position_y = Math.round(Math.random() * 150 + 50) //y轴

//产生一个四位验证码
function makeCode() {
    var code = document.getElementById("code-main")
    code.onclick = makeCode
    var string = ""
    for(let i = 0; i < 4; ++i) {
        string += createChar()
    }
    code.innerHTML = string
}

function createChar() {
    var num = Math.round(Math.random()) == 0
    if(num == true) {
        return Math.round(Math.random()*9)
    }
    else {
        var char =  String.fromCharCode(65 + Math.round(Math.random() * 25))
        if(Math.round(Math.random()) == 0) {
            return char.toLowerCase()
        }
        return char
    }
}

// console.log("slider x" + slider.offsetWidth)
// console.log("button x" + button.offsetWidth)

button.onmousedown = buttonDown;

function buttonDown(e) {
    button.style.transition = ""
    background.style.transition = ""

    var e = e || window.event || e.which;
    start_button_position = e.clientX

    document.onmousemove = buttonMove
    document.onmouseup = buttonUp
    console.log("success_length = " + success_length)
}

function buttonMove(e) {
    var button_move = e.clientX
    move_length = moveLength(button_move - start_button_position, 0, max_length)

    button.style.left = move_length + "px"
    background.style.width = move_length + "px"

    // console.log("success_length = " + success_length)
    // console.log("move_length = " + move_length)
}

function buttonUp() {
    var abs = Math.abs(move_length - success_length)
    console.log("abs = " + abs)
    if (abs >= 5) {
        button.style.left = ""
        background.style.width = ""
        button.style.transition = "left 0.8s linear"
        background.style.transition = "width 0.8s linear"
    } else {
        successMove()
    }

    document.onmousemove = null
    document.onmouseup = null
}

function moveLength(length, min, max) {
    if (length < min) {
        return min
    } else if (length > max) {
        return max
    }
    return length
}

function successMove() {
    success = true
    text.innerHTML = "解锁成功"
    background.style.backgroundColor = "lightgreen"
    // icon.className = "fas fa-thumbs-up"
    icon.className = "icon iconfont iconicon_xuanzhong"
    icon.style.color = "green"
    button.onmousedown = null
    document.onmousemove = null

    setTimeout(function() {
        alert("解锁成功")
        location.reload()
    }, 100)
}

// console.log("success_position_x = " + success_position_x)
// console.log("success_position_y = " + success_position_y)
function selectSection() {
    var img = document.getElementById("img")
    var height_string = img.getAttribute("height")
    // console.log(height_string)
    var img_height = height_string.substring(0, height_string.indexOf("px"))
    var img_url = img.getAttribute("src")

    // console.log("height = " + img_height)
    success_section.style.top = success_position_y + "px"
    success_section.style.left = success_position_x + "px"
    section.style.top = success_position_y - img_height - 3 + "px"
    section.style.background = "url(" + img_url + ") -" + success_position_x + "px -" + success_position_y + "px/auto " + img_height + "px"
    // background: url(./img/img1.jpg) 100px 200px;  
}

function randomImage() {
    var img = document.getElementById("img")
    var num = Math.ceil(Math.random()*5) + 1;
    var img_src = "./img/img"+ num +".png"
    console.log(img_src)
    img.setAttribute("src",img_src)
}

randomImage()
makeCode()
selectSection()