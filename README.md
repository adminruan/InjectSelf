# InjectSelf
一个简单的模块自注入Dome

模块注入约定
![1682581693585](https://user-images.githubusercontent.com/49390136/234795366-09c3a706-dad7-44a1-8f6a-f58734744a0c.png)

模块仅注入自己的服务
![1682581806285](https://user-images.githubusercontent.com/49390136/234795839-2159466c-48cc-4d12-aac9-f2ed12043318.jpg)

Web仅应用自己依赖的模块，使用 Microsoft.Extensions.DependencyModel 加载整个项目程序集，注入模块服务
![1682581942344](https://user-images.githubusercontent.com/49390136/234796401-b8808480-81c2-4267-97b8-2d9da9310580.png)

服务调用测试，输出正常
![1682582144288](https://user-images.githubusercontent.com/49390136/234797219-d19e58d4-e47e-476a-911f-ccdbc6147745.png)

