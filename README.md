# dotnetnb.security
无代码埋点通用权限管理系统

![](https://github.com/MingsonZheng/dotnetnb.security/blob/main/image/%E5%B1%82%E7%BA%A7%E5%88%86%E8%A7%A3.jpg)

- DotNetNB.Security.Core：定义 core，models，Istore；实现 default memory store
- DotNetNB.Security.ActionAccess：扫描 action；添加 action authorize filter；添加集成方式
- DotNetNB.Security.EntityAccess：扫描 entities；添加 ef savechanges interceptor
- DotNetNB.Security.Store.EntityFramework：基于 mysql 创建 PermissionStore 和 ResourceStore
- DotNetNB.Security.Identity：将权限赋予角色或用户；在用户登录时将 Permissions 写入用户身份 claims
- DotNetNB.WebApplication：创建 ResourceController 和 PermissionController 进行验证

## 环境配置

### mysql
```
docker pull mysql
docker run -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=root@dotnetnb666 -d mysql
```

### migration

切换到 refactor 分支
```
PM> Install-Package Microsoft.EntityFrameworkCore.Tools
PM> Update-Package Microsoft.EntityFrameworkCore.Tools
PM> Get-Help about_EntityFrameworkCore
PM> Update-Database -Context DotNetNBIdentityDbContext
PM> Update-Database -Context ApplicationDbContext
```

## 默认用户
- Username：admin
- Password：Pa$$word666

## 相关文章
- [权限系统 RGCA 架构设计](https://www.cnblogs.com/MingsonZheng/p/15824666.html)
- [权限系统 RGCA 开发任务](https://www.cnblogs.com/MingsonZheng/p/15881489.html)
- [权限系统 代码实现 ActionAccess](https://www.cnblogs.com/MingsonZheng/p/15898449.html)
- [权限系统 代码实现 EntityAccess](https://www.cnblogs.com/MingsonZheng/p/15902528.html)
- [权限系统 代码实现 Store.EntityFramework](https://www.cnblogs.com/MingsonZheng/p/15906650.html)
- [权限系统 代码实现 Identity](https://www.cnblogs.com/MingsonZheng/p/15911606.html)
