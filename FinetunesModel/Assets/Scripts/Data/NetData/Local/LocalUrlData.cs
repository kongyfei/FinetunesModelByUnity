using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using LitJson;

namespace LocalData
{
    /// <summary>
    /// ���ص�url����
    /// </summary>
    [Serializable]
    public class LocalUrlData
    {
        [Header("ID")]
        [SerializeField]
        public int id;
        [Header("URL")]
        [SerializeField]
        public string url;
        [Header("����")]
        [SerializeField]
        public string method;
        [Header("��Ӧͷ")]
        [SerializeField]
        public List<UrlHead> heads;
        [Header("��Ӧ�ֶ�")]
        [SerializeField]
        public List<UrlField> fields;
        [Header("���ݸ�ʽ")]
        [SerializeField]
        public List<UrlData> datas;

        /// <summary>
        /// ����url��head
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="formatValue">��ʽ���ַ���</param>
        /// <param name="values">ֵ</param>
        public void SetHead(string key, string formatValue = null, params object[] values)
        {
            UrlHead head = GetTheKeyValue(heads, key);

            if (head == null)
            {
                UrlHead addHead = new UrlHead();
                addHead.key = key;
                heads.Add(addHead);
                head = addHead;
            }

            if (!string.IsNullOrEmpty(formatValue))
            {
                head.value = formatValue;
            }
            else if (string.IsNullOrEmpty(head.value) && values.Length > 0)
            {
                LogExtension.LogFail($"{head.key}ȱ�ٸ�ʽ���ַ������޷�����");
                return;
            }

            if (values.Length > 0)
            {
                head.value = SetFormat(head.value, values);
            }
        }

        /// <summary>
        /// ����url�ֶ�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="type">��������</param>
        /// <param name="value">ֵ</param>
        /// <param name="values">����ֵ</param>
        public void SetField(string key, FieldType type = FieldType.None, object value = null, params object[] values)
        {
            UrlField field = GetTheKeyValue(fields, key);

            if (field == null)
            {
                UrlField addField = new UrlField();
                addField.key = key;
                fields.Add(addField);
                field = addField;
            }

            if (type != FieldType.None)
            {
                field.type = type;
            }
            else if (field.type == FieldType.None && value != null)
            {
                LogExtension.LogFail($"{field.key}�ֶ�����δ����,�޷�����");
                return;
            }

            switch (field.type)
            {
                case FieldType.String:
                    if (!string.IsNullOrEmpty((string)value))
                    {
                        string stringValue = (string)value;
                        field.stringValue = stringValue;
                    }
                    else if (string.IsNullOrEmpty(field.stringValue) && values.Length > 0)
                    {
                        LogExtension.LogFail($"{field.key}ȱ�ٸ�ʽ���ַ���,�޷�����");
                        return;
                    }

                    if (values.Length > 0)
                    {
                        string stringValue = SetFormat(field.stringValue, values);
                        field.stringValue = stringValue;
                    }
                    break;
                case FieldType.Text:
                    if (value != null)
                    {
                        TextAsset text = (TextAsset)value;
                        field.textValue = text;
                    }
                    break;
                case FieldType.Sprite:
                    if (value != null)
                    {
                        Sprite sprite = (Sprite)value;
                        field.spriteValue = sprite;
                    }
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// ����url����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="formatValue">ֵ</param>
        /// <param name="values">����</param>
        public void SetData(string key, string formatValue = null, params object[] values)
        {
            UrlData data = GetTheKeyValue(datas, key);

            if (data == null)
            {
                UrlData addData = new UrlData();
                addData.key = key;
                datas.Add(addData);
                data = addData;
            }

            if (!string.IsNullOrEmpty(formatValue))
            {
                data.value = formatValue;
            }
            else if (string.IsNullOrEmpty(data.value) && values.Length > 0)
            {
                LogExtension.LogFail($"{data.key}ȱ�ٸ�ʽ���ַ������޷�����");
                return;
            }

            if (values.Length > 0)
            {
                data.value = SetFormat(data.value, values);
            }
        }

        private string SetFormat(string content, params object[] values)
        {
            int paraCount = values.Length;
            switch (paraCount)
            {
                case 1:
                    return SetOnePara(content, (string)values[0]);
                case 2:
                    return SetTwoPara(content, (string)values[0], (string)values[1]);
            }
            return null;
        }

        private T GetTheKeyValue<T>(List<T> list, string key) where T : UrlProp
        {
            foreach (var item in list)
            {
                if (item.key == key)
                {
                    return item;
                }
            }
            return null;
        }

        private string SetOnePara(string content, string para)
        {
            string regex = @"{[0-9]}+";
            if (Regex.IsMatch(content, regex))
            {
                Debug.Log("ƥ��");
                return string.Format(content, para);
            }
            else
            {
                return content;
            }
        }

        private string SetTwoPara(string content, string para1, string para2)
        {
            return string.Format(content, para1, para2);
        }

        private void ForamtNum(string content)
        {
            int num = 0;
            string regex = @"{[0-9]}+";
            if (Regex.IsMatch(content, regex))
            {
                Debug.Log("ƥ��");
            }
            else
            {
            }
        }
    }

    [Serializable]
    public class UrlProp
    {
        public string key;
    }

    [Serializable]
    public class UrlHead : UrlProp
    {
        public string value;
    }

    [Serializable]
    public class UrlField : UrlProp
    {
        public FieldType type = FieldType.None;
        public string stringValue;
        public Sprite spriteValue;
        public TextAsset textValue;
    }

    [Serializable]
    public enum FieldType
    {
        None,
        String,
        Sprite,
        Text,
    }

    [Serializable]
    public class UrlData : UrlProp
    {
        public string value;
    }
}