var submit = document.getElementById("submit")
var char_code = document.getElementById("char-input")
var refresh = document.getElementById("refresh")
var string = "" //字符验证码

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

var img_num = 0 //图片编号

//产生一个四位验证码
function makeCode() {
    string = ""
    var code = document.getElementById("code-main")
    code.onclick = makeCode
    refresh.onclick = makeCode
    for(let i = 0; i < 4; ++i) {
        string += createChar()
    }
    code.innerHTML = string
}

//随机产生一个字符a-z,A-Z,0-9
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

function listenDown(e) {
    var e = e || window.event || e.which;
    if(e.keyCode == 13) {
        // alert("Success1")
        testCode()
    }
    // return false;
}

//提交事件
submit.onclick = testCode

function testCode() {
    // console.log("success")
    console.log(char_code.value)
    if (char_code.value == string) {
        alert("字符验证码验证成功！")
    } else {
        alert("字符验证码验证失败！")
    }
    location.reload()
}

char_code.onkeypress = listenDown
// onkeypress = " if(event.keyCode==13) { window.alert('触发事件，处理事情'); return false;}"

// console.log("slider x" + slider.offsetWidth)
// console.log("button x" + button.offsetWidth)

//button.onmousedown = buttonDown
//section.onmousedown = null
icon.onmousedown = buttonDown

//图片button点击事件
function buttonDown(e) {
    button.style.transition = ""
    background.style.transition = ""

    var e = e || window.event || e.which;
    start_button_position = e.clientX

    document.onmousemove = buttonMove
    document.onmouseup = buttonUp
    console.log("success_length = " + success_length)
}

//图片button移动事件
function buttonMove(e) {
    var button_move = e.clientX
    move_length = moveLength(button_move - start_button_position, 0, max_length)

    button.style.left = move_length + "px"
    background.style.width = move_length + 20 + "px"

    // console.log("success_length = " + success_length)
    // console.log("move_length = " + move_length)
}

//图片button鼠标松开
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

//返回一个符合规范的移动长度
function moveLength(length, min, max) {
    if (length < min) {
        return min
    } else if (length > max) {
        return max
    }
    return length
}

//成功对齐
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
        alert("图片解锁成功！")
        location.reload()
    }, 100)
}

// console.log("success_position_x = " + success_position_x)
// console.log("success_position_y = " + success_position_y)

//随机挑选图片中的一部分
function selectSection() {
    var img = document.getElementById("img")
    var height_string = img.getAttribute("height")
    // console.log(height_string)
    var img_height = height_string.substring(0, height_string.indexOf("px"))
    var img_url = img.getAttribute("src")

    // console.log("height = " + img_height)
    success_section.style.top = success_position_y + "px"
    success_section.style.left = success_position_x + "px"
    section.style.top = success_position_y - img_height - 6 + "px"
    section.style.background = "url(" + img_url + ") -" + success_position_x + "px -" + success_position_y + "px/auto " + img_height + "px"
    // background: url(./img/img1.jpg) 100px 200px;  
}

//随机产生图片地址
function randomImage() {
    var img = document.getElementById("img")
    
    var num = Math.ceil(Math.random()*5) + 1;
    img_num = (img_num + num)%5 + 2
    var img_src = "./img/img"+ img_num +".png"
    // console.log(img_src)
    console.log(img.getAttribute("src"))
    img.setAttribute("src",img_src)
}

randomImage()
makeCode()
selectSection()