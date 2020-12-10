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
    string = "" //初始验证码为空
    var code = document.getElementById("code-main") //DOM获取元素
    code.onclick = makeCode //设置code点击事件
    refresh.onclick = makeCode  //设置refresh点击事件
    for(let i = 0; i < 4; ++i) {    //总共产生4位
        string += createChar()  //每次添加一个随机位
    }
    code.innerHTML = string //修改HTML数据
}

//随机产生一个字符a-z,A-Z,0-9
function createChar() {
    var num = Math.round(Math.random()) == 0    //随机确定是字母还是数字
    if(num == true) {   //如果是数字，则返回0-9
        return Math.round(Math.random()*9)
    }
    else {  //如果是字母
        var char =  String.fromCharCode(65 + Math.round(Math.random() * 25))    //随机产生一个字母
        if(Math.round(Math.random()) == 0) {    //确定大小写
            return char.toLowerCase()   //返回小写
        }
        return char //返回大写
    }
}

function listenDown(e) {
    var e = e || window.event || e.which;
    if(e.keyCode == 13) {   //如果是回车，进行验证码验证
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
    if (char_code.value == string) {    //如果输入与随机产生相同
        alert("字符验证码验证成功！")
    } else {    //如果不同
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
    //清除样式
    button.style.transition = ""
    background.style.transition = ""

    //获取button开始位置
    var e = e || window.event || e.which;
    start_button_position = e.clientX

    document.onmousemove = buttonMove   //设置鼠标移动事件
    document.onmouseup = buttonUp   //设置鼠标松开事件
    console.log("success_length = " + success_length)
}

//图片button移动事件
function buttonMove(e) {
    var button_move = e.clientX //获取移动的位置
    //获取移动的长度
    move_length = moveLength(button_move - start_button_position, 0, max_length)

    //修改移动过程中的样式
    button.style.left = move_length + "px"
    background.style.width = move_length + 20 + "px"

    // console.log("success_length = " + success_length)
    // console.log("move_length = " + move_length)
}

//图片button鼠标松开
function buttonUp() {
    var abs = Math.abs(move_length - success_length)    //获取移动的距离和需要移动的距离的差值
    console.log("abs = " + abs)
    if (abs >= 5) { //如果超过5px的偏差，则失败，重新调整按钮位置
        button.style.left = ""
        background.style.width = ""
        //控制其缓慢移动
        button.style.transition = "left 0.8s linear"
        background.style.transition = "width 0.8s linear"
    } else {    //否则就验证成功
        successMove()
    }
    //取消事件，重新开始
    document.onmousemove = null
    document.onmouseup = null
}

//返回一个符合规范的移动长度
function moveLength(length, min, max) {
    if (length < min) { //如果小于最低要求，则返回最低要求
        return min
    } else if (length > max) {  //如果超过最高要求，则返回最高要求
        return max
    }
    return length   //正常返回
}

//成功对齐
function successMove() {
    success = true  //设置状态为true
    text.innerHTML = "解锁成功" //修改HTML内容
    background.style.backgroundColor = "lightgreen" //修改背景颜色
    // icon.className = "fas fa-thumbs-up"
    icon.className = "icon iconfont iconicon_xuanzhong" //更换图标
    icon.style.color = "green"  //设置颜色
    //取消对应事件
    button.onmousedown = null
    document.onmousemove = null

    //延时100ms后弹出结果，并在确认后刷新
    setTimeout(function() {
        alert("图片解锁成功！")
        location.reload()
    }, 100)
}

// console.log("success_position_x = " + success_position_x)
// console.log("success_position_y = " + success_position_y)

//随机挑选图片中的一部分
function selectSection() {
    var img = document.getElementById("img")    //DOM获取id为img的元素
    var height_string = img.getAttribute("height")  //获取元素的高度
    // console.log(height_string)
    var img_height = height_string.substring(0, height_string.indexOf("px"))    //获取实际像素值
    var img_url = img.getAttribute("src")   //获取图片地址

    // console.log("height = " + img_height)
    //设置成功位置的位置
    success_section.style.top = success_position_y + "px"
    success_section.style.left = success_position_x + "px"
    //进行按钮位置微调
    section.style.top = success_position_y - img_height - 6 + "px"
    //获取图片缺省部分
    section.style.background = "url(" + img_url + ") -" + success_position_x + "px -" + success_position_y + "px/auto " + img_height + "px"
    // background: url(./img/img1.jpg) 100px 200px;  
}

//随机产生图片地址
function randomImage() {
    var img = document.getElementById("img")    //获取图片元素
    
    var num = Math.ceil(Math.random()*5) + 1;   //随机产生图片序号
    img_num = (img_num + num)%5 + 2 //适配本地图片序号
    var img_src = "./img/img"+ img_num +".png"  //生成对应地址
    // console.log(img_src)
    console.log(img.getAttribute("src"))
    img.setAttribute("src",img_src) //修改图片
}

randomImage()
makeCode()
selectSection()