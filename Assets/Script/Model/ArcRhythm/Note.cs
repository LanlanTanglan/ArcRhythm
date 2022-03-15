using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using DG.Tweening;

namespace ArcRhythm
{
    public class NoteSet
    {
        public List<Note> tapNotes;
        public List<Note> pushNotes;
        public List<Note> pullNotes;
        public List<Note> longTapNotes;

        public NoteSet()
        {
            this.tapNotes = new List<Note>();
            this.pullNotes = new List<Note>();
            this.pullNotes = new List<Note>();
            this.longTapNotes = new List<Note>();
        }

        public NoteSet SetParam(JToken jt)
        {
            JArray tn = (JArray)jt["tapNotes"];
            for (int i = 0, len = tn.Count; i < len; i++)
                tapNotes.Add(new TapNote().SetParam(jt));

            JArray psn = (JArray)jt["pushNotes"];
            for (int i = 0, len = psn.Count; i < len; i++)
                tapNotes.Add(new PushNote().SetParam(jt));

            JArray pln = (JArray)jt["pullNotes"];
            for (int i = 0, len = pln.Count; i < len; i++)
                tapNotes.Add(new PullNote().SetParam(jt));

            JArray ltn = (JArray)jt["longTapNotes"];
            for (int i = 0, len = ltn.Count; i < len; i++)
                tapNotes.Add(new LongTapNote().SetParam(jt));

            return this;
        }
    }
    public class Note
    {
        public NOTE_TYPE noteType;
        public ENEMY enemy;
        public int targetOperId;
        public int attackId;
        public DIRECTION direction;
        public float speed;

        public float endTime;

        public Note()
        {

        }

        public virtual Note SetParam(JToken jt)
        {
            this.noteType = (NOTE_TYPE)(int)jt["noteType"];
            this.enemy = (ENEMY)(int)jt["enemy"];
            this.targetOperId = (int)jt["targetOperId"];
            this.attackId = (int)jt["attackId"];
            this.direction = (DIRECTION)(int)jt["direction"];
            this.speed = (float)jt["speed"];

            this.endTime = (float)jt["endTime"];

            return this;
        }
    }

    public class TapNote : Note
    {
        public bool isFake = false;
        public Vector3 fakePos;
        public TapNote()
        {

        }

        public override Note SetParam(JToken jt)
        {
            this.isFake = (bool)jt["isFake"];
            if (isFake)
            {
                this.fakePos = new Vector3((float)jt["fakePos"][0], (float)jt["fakePos"][1], (float)jt["fakePos"][2]);
            }


            return base.SetParam(jt);
        }
    }

    public class LongTapNote : Note
    {
        public float duraTime;

        public LongTapNote()
        {

        }

        public override Note SetParam(JToken jt)
        {
            this.duraTime = (float)jt["duraTime"];
            return base.SetParam(jt);
        }
    }

    public class PushNote : Note
    {
        public List<float> endTimeList;
        public List<int> targetOpers;

        public PushNote()
        {
            this.endTimeList = new List<float>();
            this.targetOpers = new List<int>();
        }

        public override Note SetParam(JToken jt)
        {
            JArray etl = (JArray)jt["endTimeList"];
            for (int i = 0; i < etl.Count; i++)
                endTimeList.Add((float)etl[i]);

            JArray ta = (JArray)jt["targetOpers"];
            for (int i = 0; i < ta.Count; i++)
                targetOpers.Add((int)ta[i]);

            return base.SetParam(jt);
        }
    }

    public class PullNote : Note
    {
        public PullNote()
        {

        }

        public override Note SetParam(JToken jt)
        {
            return base.SetParam(jt);
        }
    }


}