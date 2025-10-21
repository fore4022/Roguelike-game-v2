using System;
using System.Reflection;
using UnityEngine;
/// <summary>
/// <para>
/// ������Ʈ �ʵ忡 �����ϰ� Get/Set�� �� �ִ� ����� ���� Serializable
/// </para>
/// ���� �����ڿ� ��� ���� �ν������� �ʵ�� ���� ����
/// </summary>
[Serializable]
public class FileReference
{
    [ShowInInspector]
    public UnityEngine.Object component;
    [ShowInInspector]
    public string fieldName;

    [HideInInspector]
    public Action GetAction = null;
    [HideInInspector]
    public Action SetAction = null;

    // �̺�Ʈ ȣ��, component���� fieldName �ʵ带 ���÷������� ������ ���� ��ȯ
    public object GetValue()
    {
        if(component ==  null || string.IsNullOrEmpty(fieldName))
        {
            return null;
        }

        var field = component.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        GetAction?.Invoke();

        return field?.GetValue(component);
    }
    // �̺�Ʈ ȣ��, component���� fieldName �ʵ带 ���÷������� ������ ���� ����
    public void SetValue(object value)
    {
        if(component == null || string.IsNullOrEmpty(fieldName))
        {
            return;
        }

        var field = component.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if(field != null && CanConvert(value, field.GetValue(component).GetType()))
        {
            field.SetValue(component, value);
        }

        SetAction?.Invoke();
    }
    // ���� Ÿ������ �� ���� ���� ���θ� ��ȯ
    private bool CanConvert(object value, Type targetType)
    {
        try
        {
            var converted = Convert.ChangeType(value, targetType);

            return true;
        }
        catch
        {
            return false;
        }
    }
}