### echo

```cmd

echo $PATH 输出环境变量
echo $HOSTNAME 输出主机名
echo "Hello World" > log.txt 输出文本保存到文件 (覆盖)
echo "Hello World2" > log.txt 输出文本保存到文件 (追加)

```

## grep

1. grep

- -n 显示匹配行及行号
- -i 忽略大小写
- -r 递归查找

2. 管道命令

ps -A | grep log
