using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiliconMelter : MonoBehaviour, ISensorInOut
{
    [SerializeField] private float productSpeed;
    [SerializeField] private Construction baseCons;

    private int fuel_count;
    private List<Transform> input_list = new List<Transform>();
    private List<Transform> output_list = new List<Transform>();
    private float cur_productCD;
    public void SetUpInput(ProductSensor sensor)
    {
        if (!baseCons.isPlaced) return;

        input_list.Add(sensor.transform);
    }

    public void SetUpOutput(ProductSensor sensor)
    {
        if (!baseCons.isPlaced) return;

        output_list.Add(sensor.transform);
    }

    public void AddFuel(Props prop)
    {
        if (!baseCons.isPlaced) return;

        if (prop.Type == PropsType.Sand && fuel_count < 10)
        {
            fuel_count++;
            SimplePool.Despawn(prop);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cur_productCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!baseCons.isPlaced) return;

        if (input_list.Count > 0 && output_list.Count > 0)
        {
            if (cur_productCD <= 0)
            {
                ProduceSilicon();
                cur_productCD = productSpeed;
            }
            else
            {
                cur_productCD -= Time.deltaTime;
            }
        }
    }

    public void ProduceSilicon()
    {
        if (fuel_count >= 2)
        {
            fuel_count -= 2;
            SimplePool.Spawn<Props>(PoolType.Silicon_Props, output_list[Random.Range(0, output_list.Count)].position, Quaternion.identity);
        }
    }
}
