# dotnetnb.security
无代码埋点通用权限管理系统

![](https://github.com/MingsonZheng/dotnetnb.security/blob/main/image/%E5%B1%82%E7%BA%A7%E5%88%86%E8%A7%A3.jpg)

- DotNetNB.Security.Core：定义 core，models，Istore；实现 default memory store
- DotNetNB.Security.ActionAccess：扫描 action；添加 action authorize filter；添加集成方式
- DotNetNB.Security.EntityAccess：扫描 entities；添加 ef savechanges interceptor
- DotNetNB.Security.Store.EntityFramework：基于 mysql 创建 PermissionStore 和 ResourceStore
- DotNetNB.Security.Identity：将权限赋予角色或用户；在用户登录时将 Permissions 写入用户身份 claims
- DotNetNB.WebApplication：创建 ResourceController 和 PermissionController 进行验证
