using QFramework;
using UnityEngine;

public class EnemyDieFx : MonoBehaviour
{
    // 提高性能 通过ID获取属性
    private static readonly int fade = Shader.PropertyToID("_Fade");

    // 公开的材质
    public Material Material;

    private void Start()
    {
        // 生成材质
        var material = Instantiate(Material);
        // 设置材质
        GetComponent<SpriteRenderer>().material = material;
        ActionKit.Lerp(1, 0, 0.5f, value =>
            {
                material.SetFloat(fade, value);
                // 放大
                this.LocalScale(1 + (1 - value) * 0.5f, 1 + (1 - value) * 0.5f);
            })
            .Start(this, () => Destroy(gameObject));
    }
}