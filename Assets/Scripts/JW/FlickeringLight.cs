using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlickeringLight : MonoBehaviour
{
    public Light spotlight;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    [SerializeField] private float defaultIntensity;
    public float noiseScale = 1.0f; // ������ ������
    public int steps = 5; // �������� ��Ȯ�� �ܰ� ��

    private float randomStart;

    void Start()
    {
        StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {
        while (true)
        {
            int times = 0;
            times = Random.Range(2, 4);
            for (int now = 0; now <= times; now++) // ���� �ݺ�
            {
                spotlight.intensity = 0.1f;
                float a = Random.Range(0.01f, 0.08f);
                yield return new WaitForSeconds(a);
                spotlight.intensity = defaultIntensity;
                a = Random.Range(0.01f, 0.08f);
                yield return new WaitForSeconds(a);
            }
            yield return new WaitForSeconds(Random.Range(4f, 8f));
        }
    }
}
