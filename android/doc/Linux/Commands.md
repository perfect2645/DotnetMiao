### echo

```cmd

echo $PATH 输出环境变量
echo $HOSTNAME 输出主机名
echo "Hello World" > log.txt 输出文本保存到文件 (覆盖)
echo "Hello World2" > log.txt 输出文本保存到文件 (追加)

```

## cd

cd / 切换到根目录
cd ~ 切换到自己的用户目录（安卓切换到根目录）
su 进入 root
pwd 显示当前路径

## ls

ls 后面可以跟路径
ls -a 显示所有文件包括隐藏文件
ls -l 显示文件详细信息
ls -h 显示统计
du -h 显示文件大小

## file

- 显示文件详细类型
- 支持通配符
  file frida\*

## grep

1. grep

- -n 显示匹配行及行号
- -i 忽略大小写
- -r 递归查找

2. 管道命令

ps -A | grep log
