using UnityEngine;


[ExecuteInEditMode]
public class UnlitProps : MonoBehaviour
{
    private static int _colorPropertyID = -1;
    private int ColorPropertyID
    {
        get
        {
            if (_colorPropertyID < 0)
            {
                _colorPropertyID = Shader.PropertyToID("_Color");
            }
            return _colorPropertyID;
        }
    }

    private static MaterialPropertyBlock _matblock;
    /// <summary>
    /// 共用一个，减少New
    /// </summary>
    private static MaterialPropertyBlock MatBlock
    {
        get
        {
            if (_matblock == null)
            {
                _matblock = new MaterialPropertyBlock();
            }
            return _matblock;
        }
    }

    [SerializeField]
    private Color _color = Color.white;
    private MeshRenderer _mat;


    private void OnEnable()
    {
        if (!_mat)
        {
            _mat = gameObject.GetComponent<MeshRenderer>();
        }
        // _mat.GetPropertyBlock(MatBlock);
        // 如果需要可以进行Clear的操作
        // MatBlock.Clear();
        MatBlock.SetColor(ColorPropertyID, _color);
        _mat.SetPropertyBlock(MatBlock);
    }

    private void Awake()
    {
#if UNITY_EDITOR
        _tempColor = _color;
#endif
    }

#if UNITY_EDITOR
    private Color _tempColor;
    private void Update()
    {
        if (_tempColor != _color)
        {
            OnEnable();
        }
    }
#endif
}
