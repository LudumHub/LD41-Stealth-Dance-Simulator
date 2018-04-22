using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StandardAnimations
{
    private static StandardAnimationsInstance instanceHolder;

    private static StandardAnimationsInstance Instance
    {
        get
        {
            if (null == instanceHolder)
            {
                var obj = GameObject.Find("StandardAnimations");
                if (null == obj)
                {
                    obj = new GameObject("StandardAnimations");
                }

                GameObject.DontDestroyOnLoad(obj);
                instanceHolder = obj.GetComponent<StandardAnimationsInstance>();
                if (null == instanceHolder)
                {
                    instanceHolder = obj.AddComponent<StandardAnimationsInstance>();
                }
            }

            return instanceHolder;
        }
    }

    public static void DoVibration(GameObject target,
                                   string callback = null,
                                   bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.Vibration(target, callback, isoverride));
    }

    public static void DoAppearance(GameObject target,
                                    float time,
                                    string callback = null,
                                    bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.AppearanceRecursive(target,
                                                             time,
                                                             callback,
                                                             isoverride));
    }

    public static void DoAppearanceUI(GameObject target,
                                      float time,
                                      string callback = null,
                                      bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.AppearanceUIRecursive(target,
                                                               time,
                                                               callback,
                                                               isoverride));
    }

    public static void DoVanishing(GameObject target,
                                   float time,
                                   string callback = null,
                                   bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.VanishingRecursive(target,
                                                            time,
                                                            target,
                                                            callback,
                                                            isoverride));
    }

    public static void DoChangeAlpha(SpriteRenderer target,
                                     float time,
                                     float valueStart,
                                     float valueFinish,
                                     GameObject callbackObject,
                                     string callback = null,
                                     bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.ChangeAlpha(target,
                                                     time,
                                                     valueStart,
                                                     valueFinish,
                                                     callbackObject,
                                                     callback,
                                                     isoverride));
    }

    public static void DoChangeAlpha(SpriteRenderer target,
                                     float time,
                                     float valueStart,
                                     float valueFinish,
                                     string callback = null,
                                     bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.ChangeAlpha(target,
                                                     time,
                                                     valueStart,
                                                     valueFinish,
                                                     target.gameObject,
                                                     callback,
                                                     isoverride));
    }

    public static void DoVanishingUI(GameObject target,
                                     float time,
                                     string callback = null,
                                     bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.VanishingUIRecursive(target,
                                                              time,
                                                              callback,
                                                              isoverride));
    }

    public static void DoVanishing(GameObject target,
                                   float time,
                                   GameObject callbackObject,
                                   string callback = null,
                                   bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.VanishingRecursive(target,
                                                            time,
                                                            callbackObject,
                                                            callback,
                                                            isoverride));
    }

    public static void DoMoveFromTo(GameObject target,
                                    Vector3 posStart,
                                    Vector3 posFinish,
                                    float speed,
                                    string callback = null,
                                    bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.MoveFromTo(target,
                                                    posStart,
                                                    posFinish,
                                                    speed,
                                                    target,
                                                    callback,
                                                    isoverride));
    }

    public static void DoMoveFromTo(GameObject target,
                                    Vector3 posStart,
                                    Vector3 posFinish,
                                    float speed,
                                    GameObject callbackObject,
                                    string callback = null,
                                    bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.MoveFromTo(target,
                                                    posStart,
                                                    posFinish,
                                                    speed,
                                                    callbackObject,
                                                    callback,
                                                    isoverride));
    }

    public static void DoRotate(GameObject target,
                                Vector3 angle,
                                float time,
                                GameObject callbackObject,
                                string callback = null,
                                bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.Rotate(target,
                                                angle,
                                                time,
                                                callbackObject,
                                                callback,
                                                isoverride));
    }

    public static void DoRotate(GameObject target,
                                Vector3 angle,
                                float time,
                                string callback = null,
                                bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.Rotate(target,
                                                angle,
                                                time,
                                                target,
                                                callback,
                                                isoverride));
    }

    public static void DoScale(GameObject target,
                               GameObject callbackObject,
                               string callback = null,
                               bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.Scale(target,
                                               callbackObject,
                                               callback,
                                               isoverride));
    }

    public static void DoScale(GameObject target,
                                string callback = null,
                                bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.Scale(target,
                                               target,
                                               callback,
                                               isoverride));
    }

    public static void DoMoveFromToTime(GameObject target,
                                        Vector3 posStart,
                                        Vector3 posFinish,
                                        float time,
                                        string callback = null,
                                        bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.MoveFromToTime(target,
                                                        posStart,
                                                        posFinish,
                                                        time,
                                                        target,
                                                        callback,
                                                        isoverride));
    }

    public static void DoMoveFromToTime(GameObject target,
                                        Vector3 posStart,
                                        Vector3 posFinish,
                                        float time,
                                        GameObject callbackObject,
                                        string callback = null,
                                        bool isoverride = false)
    {
        Instance.StartCoroutine(Instance.MoveFromToTime(target,
                                                        posStart,
                                                        posFinish,
                                                        time,
                                                        callbackObject,
                                                        callback,
                                                        isoverride));
    }

    internal class StandardAnimationsInstance : MonoBehaviour
    {
        private HashSet<GameObject> ObjectsVibration;
        private HashSet<GameObject> ObjectsAppearance;
        private HashSet<GameObject> ObjectsVanishing;
        private HashSet<GameObject> ObjectsMoveFromTo;
        private HashSet<GameObject> ObjectsRotate;
        private HashSet<GameObject> ObjectsChangeAlpha;
        
        public IEnumerator Vibration(GameObject target,
                                     string callback,
                                     bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsVibration)
                {
                    ObjectsVibration = new HashSet<GameObject>();
                }
                bool contains = ObjectsVibration.Contains(target);
                if (!contains)
                {
                    ObjectsVibration.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                GameObject obj = target;

                float t;
                float time = 0.01f;
                Vector3 delta = new Vector3(0.1f, 0.01f, 0.0f);
                Vector3 positionStart = obj.transform.position;
                Vector3 positionRight = positionStart + delta;
                Vector3 positionLeft = positionStart - delta;

                for (int i = 0; i < 3; ++i)
                {
                    t = 0.0f;
                    while (t < 1.0f)
                    {
                        obj.transform.position = new Vector2(Mathf.Lerp(positionStart.x, positionRight.x, t),
                                                         Mathf.Lerp(positionStart.y, positionRight.y, t));
                        t += Time.deltaTime / time;
                        yield return null;
                    }

                    obj.transform.position = positionRight;

                    t = 0.0f;
                    while (t < 1.0f)
                    {
                        obj.transform.position = new Vector2(Mathf.Lerp(positionRight.x, positionLeft.x, t),
                                                         Mathf.Lerp(positionRight.y, positionLeft.y, t));
                        t += Time.deltaTime / (2.0f * time);
                        yield return null;
                    }

                    obj.transform.position = positionLeft;

                    t = 0.0f;
                    while (t < 1.0f)
                    {
                        obj.transform.position = new Vector2(Mathf.Lerp(positionLeft.x, positionStart.x, t),
                                                         Mathf.Lerp(positionLeft.y, positionStart.y, t));
                        t += Time.deltaTime / time;
                        yield return null;
                    }

                    obj.transform.position = positionStart;
                }
            }

            if (!isoverride)
            {
                ObjectsVibration.Remove(target);
            }

            if (!string.IsNullOrEmpty(callback))
            {
                target.SendMessage(callback, doAction);
            }
        }

        public IEnumerator AppearanceRecursive(GameObject target,
                                               float time,
                                               string callback,
                                               bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsAppearance)
                {
                    ObjectsAppearance = new HashSet<GameObject>();
                }
                bool contains = ObjectsAppearance.Contains(target);
                if (!contains)
                {
                    ObjectsAppearance.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                //StartCoroutine(Appearance(target.GetComponent<SpriteRenderer>(), time));
                SpriteRenderer[] renderers = target.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0, count = renderers.Length; i < count; ++i)
                {
                    var renderer = renderers[i];
                    StartCoroutine(Appearance(renderer, time));
                }
            }

            if (!isoverride)
            {
                ObjectsAppearance.Remove(target);
            }

            if (!string.IsNullOrEmpty(callback))
            {
                if (doAction)
                {
                    yield return new WaitForSeconds(time);
                }
                target.SendMessage(callback, doAction);
            }
        }

        public IEnumerator AppearanceUIRecursive(GameObject target,
                                                 float time,
                                                 string callback,
                                                 bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsAppearance)
                {
                    ObjectsAppearance = new HashSet<GameObject>();
                }
                bool contains = ObjectsAppearance.Contains(target);
                if (!contains)
                {
                    ObjectsAppearance.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                //StartCoroutine(AppearanceUI(target.GetComponent<UnityEngine.UI.Image>(), time));
                UnityEngine.UI.Image[] images = target.GetComponentsInChildren<UnityEngine.UI.Image>();
                for (int i = 0, count = images.Length; i < count; ++i)
                {
                    var img = images[i];
                    StartCoroutine(AppearanceUI(img, time));
                }
                //StartCoroutine(AppearanceText(target.GetComponent<UnityEngine.UI.Text>(), time));
                UnityEngine.UI.Text[] texts = target.GetComponentsInChildren<UnityEngine.UI.Text>();
                for (int i = 0, count = texts.Length; i < count; ++i)
                {
                    var txt = texts[i];
                    StartCoroutine(AppearanceText(txt, time));
                }
            }

            if (!isoverride)
            {
                ObjectsAppearance.Remove(target);
            }

            if (!string.IsNullOrEmpty(callback))
            {
                if (doAction)
                {
                    yield return new WaitForSeconds(time);
                }
                target.SendMessage(callback, doAction);
            }
        }

        private IEnumerator Appearance(SpriteRenderer renderer,
                                       float time)
        {
            if (renderer != null)
            {
                float a = renderer.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    renderer.color = new Color(renderer.color.r,
                                               renderer.color.g,
                                               renderer.color.b,
                                               Mathf.Lerp(0.0f, a, t));
                    yield return null;
                }

                renderer.color = new Color(renderer.color.r,
                                           renderer.color.g,
                                           renderer.color.b,
                                           a);
            }
        }

        private IEnumerator AppearanceUI(UnityEngine.UI.Image image,
                                         float time)
        {
            if (image != null)
            {
                float a = image.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    image.color = new Color(image.color.r,
                                            image.color.g,
                                            image.color.b,
                                            Mathf.Lerp(0.0f, a, t));
                    yield return null;
                }

                image.color = new Color(image.color.r,
                                        image.color.g,
                                        image.color.b,
                                        a);
            }
        }

        private IEnumerator AppearanceText(UnityEngine.UI.Text txt,
                                           float time)
        {
            if (txt != null)
            {
                float a = txt.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    txt.color = new Color(txt.color.r,
                                          txt.color.g,
                                          txt.color.b,
                                          Mathf.Lerp(0.0f, a, t));
                    yield return null;
                }

                txt.color = new Color(txt.color.r,
                                      txt.color.g,
                                      txt.color.b,
                                      a);
            }
        }

        public IEnumerator VanishingRecursive(GameObject target,
                                              float time,
                                              GameObject callbackObject,
                                              string callback,
                                              bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsVanishing)
                {
                    ObjectsVanishing = new HashSet<GameObject>();
                }
                bool contains = ObjectsVanishing.Contains(target);
                if (!contains)
                {
                    ObjectsVanishing.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                //StartCoroutine(Vanishing(target.GetComponent<SpriteRenderer>(), time));
                SpriteRenderer[] renderers = target.GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0, count = renderers.Length; i < count; ++i)
                {
                    var renderer = renderers[i];
                    StartCoroutine(Vanishing(renderer, time));
                }
            }

            if (!isoverride)
            {
                ObjectsVanishing.Remove(target);
            }

            if (!string.IsNullOrEmpty(callback))
            {
                if (doAction)
                {
                    yield return new WaitForSeconds(time);
                }
                callbackObject.SendMessage(callback, doAction);
            }
        }

        public IEnumerator VanishingUIRecursive(GameObject target,
                                                 float time,
                                                 string callback,
                                                 bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsVanishing)
                {
                    ObjectsVanishing = new HashSet<GameObject>();
                }
                bool contains = ObjectsVanishing.Contains(target);
                if (!contains)
                {
                    ObjectsVanishing.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                //StartCoroutine(VanishingUI(target.GetComponent<UnityEngine.UI.Image>(), time));
                UnityEngine.UI.Image[] images = target.GetComponentsInChildren<UnityEngine.UI.Image>();
                for (int i = 0, count = images.Length; i < count; ++i)
                {
                    var img = images[i];
                    StartCoroutine(VanishingUI(img, time));
                }
                //StartCoroutine(VanishingText(target.GetComponent<UnityEngine.UI.Text>(), time));
                UnityEngine.UI.Text[] texts = target.GetComponentsInChildren<UnityEngine.UI.Text>();
                for (int i = 0, count = texts.Length; i < count; ++i)
                {
                    var txt = texts[i];
                    StartCoroutine(VanishingText(txt, time));
                }
            }

            if (!isoverride)
            {
                ObjectsVanishing.Remove(target);
            }

            if (!string.IsNullOrEmpty(callback))
            {
                if (doAction)
                {
                    yield return new WaitForSeconds(time);
                }
                target.SendMessage(callback, doAction);
            }
        }

        private IEnumerator Vanishing(SpriteRenderer renderer,
                                      float time)
        {
            if (renderer != null)
            {
                float a = renderer.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    renderer.color = new Color(renderer.color.r,
                                               renderer.color.g,
                                               renderer.color.b,
                                               Mathf.Lerp(a, 0.0f, t));
                    yield return null;
                }

                renderer.color = new Color(renderer.color.r,
                                           renderer.color.g,
                                           renderer.color.b,
                                           0.0f);
            }
        }

        public IEnumerator ChangeAlpha(SpriteRenderer target,
                                       float time,
                                       float valueStart,
                                       float valueFinish,
                                       GameObject callbackObject,
                                       string callback,
                                       bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsChangeAlpha)
                {
                    ObjectsChangeAlpha = new HashSet<GameObject>();
                }
                bool contains = ObjectsChangeAlpha.Contains(target.gameObject);
                if (!contains)
                {
                    ObjectsChangeAlpha.Add(target.gameObject);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                var renderer = target;

                var t = 0f;
                while (t < 1f)
                {
                    renderer.color = new Color(renderer.color.r,
                                               renderer.color.g,
                                               renderer.color.b,
                                               Mathf.Lerp(valueStart, valueFinish, t));
                    t += Time.deltaTime / time;
                    yield return null;
                }

                renderer.color = new Color(renderer.color.r,
                                           renderer.color.g,
                                           renderer.color.b,
                                           valueFinish);

                if (!isoverride)
                {
                    ObjectsChangeAlpha.Remove(target.gameObject);
                }
            }

            if (callbackObject != null &&
                !string.IsNullOrEmpty(callback))
            {
                callbackObject.SendMessage(callback, target);
            }
        }

        private IEnumerator VanishingUI(UnityEngine.UI.Image image,
                                         float time)
        {
            if (image != null)
            {
                float a = image.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    image.color = new Color(image.color.r,
                                            image.color.g,
                                            image.color.b,
                                            Mathf.Lerp(a, 0.0f, t));
                    yield return null;
                }

                image.color = new Color(image.color.r,
                                        image.color.g,
                                        image.color.b,
                                        0.0f);
            }
        }

        private IEnumerator VanishingText(UnityEngine.UI.Text txt,
                                           float time)
        {
            if (txt != null)
            {
                float a = txt.color.a;

                for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / time)
                {
                    txt.color = new Color(txt.color.r,
                                            txt.color.g,
                                            txt.color.b,
                                            Mathf.Lerp(a, 0.0f, t));
                    yield return null;
                }

                txt.color = new Color(txt.color.r,
                                        txt.color.g,
                                        txt.color.b,
                                        0.0f);
            }
        }

        public IEnumerator MoveFromTo(GameObject target,
                                      Vector3 posStart,
                                      Vector3 posFinish,
                                      float speed,
                                      GameObject callbackObject,
                                      string callback,
                                      bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsMoveFromTo)
                {
                    ObjectsMoveFromTo = new HashSet<GameObject>();
                }
                bool contains = ObjectsMoveFromTo.Contains(target);
                if (!contains)
                {
                    ObjectsMoveFromTo.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                target.transform.localPosition = posStart;

                var time = (posFinish - posStart).magnitude / speed;

                var t = 0f;
                while (t < 1f)
                {
                    target.transform.localPosition = Vector3.Lerp(posStart, posFinish, t);
                    t += Time.deltaTime / time;
                    yield return null;
                }
                target.transform.localPosition = posFinish;

                if (!isoverride)
                {
                    ObjectsMoveFromTo.Remove(target);
                }
            }

            if (callbackObject != null &&
                !string.IsNullOrEmpty(callback))
            {
                callbackObject.SendMessage(callback, doAction);
            }
        }

        public IEnumerator MoveFromToTime(GameObject target,
                                          Vector3 posStart,
                                          Vector3 posFinish,
                                          float time,
                                          GameObject callbackObject,
                                          string callback,
                                          bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsMoveFromTo)
                {
                    ObjectsMoveFromTo = new HashSet<GameObject>();
                }
                bool contains = ObjectsMoveFromTo.Contains(target);
                if (!contains)
                {
                    ObjectsMoveFromTo.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                target.transform.localPosition = posStart;

                var t = 0f;
                while (t < 1f)
                {
                    target.transform.localPosition = Vector3.Lerp(posStart, posFinish, t);
                    t += Time.deltaTime / time;
                    yield return null;
                }
                target.transform.localPosition = posFinish;

                if (!isoverride)
                {
                    ObjectsMoveFromTo.Remove(target);
                }
            }

            if (callbackObject != null &&
                !string.IsNullOrEmpty(callback))
            {
                callbackObject.SendMessage(callback, doAction);
            }
        }

        public IEnumerator Rotate(GameObject target,
                                  Vector3 angle,
                                  float time,
                                  GameObject callbackObject,
                                  string callback,
                                  bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsRotate)
                {
                    ObjectsRotate = new HashSet<GameObject>();
                }
                bool contains = ObjectsRotate.Contains(target);
                if (!contains)
                {
                    ObjectsRotate.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                var originalRotation = target.transform.localRotation;

                var t = 0f;
                while (t < 1f)
                {
                    var dt = Time.deltaTime / time;
                    target.transform.Rotate(angle * dt);
                    t += dt;
                    yield return null;
                }

                target.transform.localRotation = originalRotation;
                target.transform.Rotate(angle);

                if (!isoverride)
                {
                    ObjectsRotate.Remove(target);
                }
            }

            if (callbackObject != null &&
                !string.IsNullOrEmpty(callback))
            {
                callbackObject.SendMessage(callback, doAction);
            }
        }

        public IEnumerator Scale(GameObject target,
                                  GameObject callbackObject,
                                  string callback,
                                  bool isoverride)
        {
            bool doAction = true;
            if (!isoverride)
            {
                if (null == ObjectsRotate)
                {
                    ObjectsRotate = new HashSet<GameObject>();
                }
                bool contains = ObjectsRotate.Contains(target);
                if (!contains)
                {
                    ObjectsRotate.Add(target);
                }
                else
                {
                    doAction = false;
                }
            }

            if (doAction)
            {
                float t;
                float time = 0.1f;
                Vector3 scaleStart = target.transform.localScale;
                Vector3 scaleFinish = 1.1f * scaleStart;

                t = 0.0f;
                while (t < 1.0f)
                {
                    target.transform.localScale = new Vector3(Mathf.Lerp(scaleStart.x, scaleFinish.x, t),
                                                              Mathf.Lerp(scaleStart.y, scaleFinish.y, t));
                    t += Time.deltaTime / (time / 2.0f);
                    yield return null;
                }

                transform.localScale = scaleFinish;

                t = 0.0f;
                while (t < 1.0f)
                {
                    target.transform.localScale = new Vector3(Mathf.Lerp(scaleFinish.x, scaleStart.x, t),
                                                              Mathf.Lerp(scaleFinish.y, scaleStart.y, t));
                    t += Time.deltaTime / (time / 2.0f);
                    yield return null;
                }

                target.transform.localScale = scaleStart;

                if (!isoverride)
                {
                    ObjectsRotate.Remove(target);
                }
            }

            if (callbackObject != null &&
                !string.IsNullOrEmpty(callback))
            {
                callbackObject.SendMessage(callback, doAction);
            }
        }
    }
}