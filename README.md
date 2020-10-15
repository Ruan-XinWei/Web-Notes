- [Web-Notes](#web-notes)
  - [MVC基础编程](#mvc基础编程)
  - [前后端数据交换](#前后端数据交换)
    - [前端数据提交](#前端数据提交)
    - [后台数据发送](#后台数据发送)
# Web-Notes
## MVC基础编程

1. 新建一个MVC项目
   1. 选择ASP . NET Web应用程序(.NET Framework)
   2. 设置项目名称和存储位置
   3. 选择MVC模式，可以添加身份验证（一般选择个人用户账户）
   4. 初次运行，需要信任IIS Express SSL，然后不安装证书
2. MVC项目目录结构
   1. 引用：支持应用系统运行所需的类库（如.dll文件）
   2. App_Data：存放私有数据，如数据库文件。
   3. App_Start：存放项目的核心配置，如路由配置等。
   4. Content：存放CSS、图像等内容。
   5. Controllers：存放Controller类，处理URL请求。
   6. Models：存放与业务相关的实体类数据模型。
   7. Scripts：存放JavaScript类库文件和其他.js文件。
   8. Views：存放视图文件（按控制器名称分组）。默认首页视图：Views/Home/Index.cshtml，由Controllers/HomeController.cs控制。
   9. Views/Shared：主要存放布局文件(如_Layout.cshtml)、自定义视图或公共视图。
   10. Views/_ViewStart.cshtml：通常用于指定视图默认的布局页。
   11. Global.asax：提供全局可用代码。
   12. Web.config：应用系统的全局配置文件。（注意Views下也可有局部配置文件）
3.  MVC模式概念：
    1.  模型Model：表示应用数据的类
    2.  控制器Controller：处理浏览器的传入请求，检索模型数据，将响应返回给浏览器
    3.  视图View：显示结果的用户界面
4.  添加控制器
    1.  右键Controllers文件夹
    2.  选择添加控制器
    3.  选择 MVC5控制器-空
    4.  命名为xxxController（后缀为Controller）
5.  控制器代码
    1.  默认代码  
    ```cs
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
    ```
    2. 添加和修改Action代码,运行URL为https://localhost:44300/Default/index1  
    ```cs
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public string Index1()
        {
            return "测试文本";
        }
    }
    ```
 5.  视图
     1.  不使用布局页
         1. 在方法里面右键添加视图
         
         2. 视图名称默认与方法同名（如果需要不同名字的布局页，在需要调用时，就需要在此方法返回改成`return View("布局名")`），并且不使用布局页
         
         3. 添加之后创建的视图会自动存放在`Views/Controller名文件夹`中
         
         4. 视图默认代码就是一个完整的HTML文档，但是后缀名是.cshtml
         
         5. 视图默认代码  
         ```cshtml
            @{
                Layout = null;
            }
        
            <!DOCTYPE html>
        
            <html>
            <head>
                <meta name="viewport" content="width=device-width" />
                <title>Index</title>
            </head>
            <body>
                <div> 
                </div>
            </body>
            </html>
         ```
         6.  运行起来就是这个代码显示情况
     2.  使用布局页（大部分和不使用布局页类似）
         1.  在方法里面右键添加视图
         2.  在这里使用布局页，可以选择自定义的布局页，如果不选择就是系统默认的布局页
         3.  使用布局页的默认代码，这时候如果想要添加页面代码，就只需要添加body标签内代码即可  
            ```cshtml
            @{
               ViewBag.Title = "Index2";
            }
           
            <h2>Index2</h2>
            ```
         4. 使用布局页之后，就会将视图代码嵌入到布局页（_Layout.cshtml，默认的布局页位于Views/Shared文件夹）中的`@RenderBody()`上，这个功能就是子视图占位
        
         5. 添加一个菜单项  
        
            ```cshtml
             <li>@Html.ActionLink("标签内容", "方法名", "Controller名")</li>
            ```
        
    
6. _ViewStart.cshtml
   1. 会在所有视图被执行之前先执行，通常用于指定视图默认的布局页  
   
      ```cshtml
      @{
       Layout = "~/Views/Shared/_Layout.cshtml";
       }
      ```
   
   2. 局部与全局
      1. 全局：所有视图都起作用，位于Views文件夹下
      2. 局部：与视图文件一起位于某个子文件夹下，该文件夹下的视图将受局部_ViewStart.cshtml作用。

## 前后端数据交换

### 前端数据提交

1. 前端使用Form表单将数据提交到后端，在MVC中，Form的action需要按照`控制器名称/对应的方法`这样的格式，提交到对应的方法中  
    ```html
    <form action="/Default/ShowInfo" method="POST" id="form" enctype="multipart/form-data">
    </form>
    ```
    ```cs
    public class DefaultController : Controller {
        public ActionResult ShowInfo() {
            //...
        }
    }
    ```
2. 提交给对应的方法后，方法直接在形参列表中写出前端输入框的name来进行获取输入框的值  
    ```html
    <form action="/Default/ShowInfo" method="POST" id="form" enctype="multipart/form-data">
        <input type="text" name="phone" id="phone" placeholder="11位手机号">
    </form>
    ```
    ```cs
    public class DefaultController : Controller {
        public ActionResult ShowInfo(string phone) {
            //...
        }
    }
    ```

### 后台数据发送
1. 在后台先将数据存储到ViewBag中  
    ```cs
    public class DefaultController : Controller {
        public ActionResult ShowInfo(string phone) {
            ViewBag.phone = phone;
        }
    }
    ```
2. 前端通过ViewBag进行获取  
    ```cshtml
    <p>手机号码：@ViewBag.phone;</p>
    ```