using System.Net.NetworkInformation;
namespace ArcRhythm
{
    [System.Serializable]
    public class GamePlayResult
    {
        public string bmsName;//铺面名称
        public int totalNoteNum;//这个铺面的总共的物量 TODO 考虑长按条的物量(算为1)
        public float accuracyRate;//准确度
        public int score;//分数
        public int goodNum;
        public int badNum;
        public int perfectNum;
        public int missNum;
        public int maxCombo;//最大连击数
        public int resultLevel;//结果等级,D,C,B,A,S,V,(至臻--主题，危机合约等徽章改成金黄色)

        public int currentMaxCombo;

        public GamePlayResult addGoodNum()
        {
            this.goodNum++;
            return this;
        }
        public GamePlayResult addPerfectNum()
        {
            this.perfectNum++;
            return this;
        }
        public GamePlayResult addBadNum()
        {
            this.badNum++;
            return this;
        }
        public GamePlayResult addMissNum()
        {
            this.missNum++;
            return this;
        }

        public GamePlayResult addMaxNum(bool m)
        {
            if (m) currentMaxCombo++;
            else currentMaxCombo = 0;
            if (currentMaxCombo > maxCombo) maxCombo = currentMaxCombo;

            RefreshResult();
            return this;
        }

        //更新结果
        public void RefreshResult()
        {
            this.score = (int)((9 * this.perfectNum + 5.85f * this.goodNum + this.maxCombo) / this.totalNoteNum * 100000);
        }

        public GamePlayResult setTotalNoteNum(int t)
        {
            this.totalNoteNum = t;
            return this;
        }

        public GamePlayResult()
        {
            this.badNum = 0;
            this.goodNum = 0;
            this.perfectNum = 0;
            this.missNum = 0;
            this.maxCombo = 0;
            this.currentMaxCombo = 0;
        }

        public GamePlayResult ClearInfo()
        {
            this.badNum = 0;
            this.goodNum = 0;
            this.perfectNum = 0;
            this.missNum = 0;
            this.maxCombo = 0;
            this.currentMaxCombo = 0;

            return this;
        }
    }
}