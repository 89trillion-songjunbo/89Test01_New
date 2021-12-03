# 89Test01技术文档

1、功能概述

- 实现Unity列表赋值刷新，特效动画的展示
- 涉及知识点：Json数据的读取，Unity的界面的刷新，及Unity简单动画的制作



2、整体功能的实现思路

- 采用常用的界面，逻辑分开处理的思路

- 启动器首先初始化数据类，数据处理完成后，打开对应界面，进行赋值刷新逻辑。

- 同时注册各个界面Button对应的事件



3、资源目录结构

| 目录名称   | 目录内容                           | 父目录       |
| ---------- | ---------------------------------- | ------------ |
| Resources  | 存放动态加载需要的资源             | Assets根目录 |
| Data       | json数据存放位置                   | Resources    |
| Effect     | 特效存放目录                       | Resources    |
| Prefabs/UI | Prefabs存放预制体/UI存放sprite信息 | Resources    |
| Scripts    | 存放脚本                           | Assets根目录 |
| Main       | 启动器及工具存放文件夹             | Scripts      |
| Model      | 数据类                             | Scripts      |
| View       | 视图类                             | Scripts      |
| Plugins    | 存放插件                           | Assets根目录 |
| DOTween    | 动画插件                           | Plugins      |
| SimpleJson | 解析json文件插件                   | Plugins      |



4、界面对象结构拆分

| 结构        | 结构对象说明 | 父界面对象 | 其他说明 |      |      |
| ----------- | ------------ | ---------- | -------- | ---- | ---- |
| SampleScene | 主场景       |            |          |      |      |
| MainWindow  | 主界面       | Canvas     |          |      |      |
| Button      | 启动按钮     | Canvas     |          |      |      |



5、代码逻辑分层

| 类                           | 主要职责         | 其他说明Main                                     |
| ---------------------------- | ---------------- | ------------------------------------------------ |
| MainController               | 用于启动游戏     | 初始化界面所需要的数据信息，注册启动按钮事件     |
| UiMainWindowModel            | 主界面对应数据类 | 管理界面所需要的数据，主界面需要数据都从这里获取 |
| UiMainWindowView             | 主界面UI类       | 由于管理ui的刷新，初始化Item子类使用             |
| RewardBoxPanel/DailyBoxPanel | 宝箱类           | 对应活动类型，用于UI逻辑刷新                     |
| Tools                        | 工具类           | 资源加载，ui特殊处理工具类                       |
| Const                        | 常量类           | 路径信息等常量信息                               |



6、数据解析

- 通过json数据设计对应类型，通过SimpleJson进行数据解析操作，存于界面对应的数据管理类备用


| 宝箱id    | 宝箱类型 | 子类型  | 数量 | 花费金币 | 领取状态    |
| --------- | -------- | ------- | ---- | -------- | ----------- |
| ProductId | Type     | SubType | Num  | CostGold | IsPurchased |



7、关键代码逻辑的流程图

![Image](https://github.com/89trillion-songjunbo/89Test01_New/blob/main/89Test01%20脚本流程图.png)

8、运行流程图

![Image](https://github.com/89trillion-songjunbo/89Test01/blob/master/89Test01.png)



