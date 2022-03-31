namespace ArcRhythm
{
    /// <summary>
    /// 路径集合
    /// 某人路径要么是直接带文件
    /// 要么是父级目录(即最后一个字母一定是 / )
    /// </summary>
    public static class ArcPath
    {
        public static string prefebDirOfOperator = "Prefab/Operator/";//干员预制体地址
        public static string prefebDirOfEnemy = "Prefab/Enemy/";//干员预制体地址
    }

    public static class ArcStr
    {
        public static string operatorName = "Operator";
    }

    public static class ArcNum
    {
        public static float defaultBeginTime = -5f;
        public static int pixelPreUnit = 100;
        public static float perJudgeTime = 0.08f;//判定单位格子宽度
        public static float badJudgeTime = 0.03f;//判定失败单位格子宽度
        public static float prJudgeTime = -(perJudgeTime * 2 + badJudgeTime);//判定区间
        public static float neJudgeTime = 2 * perJudgeTime;//判定区间
    }
}