using System.Collections;
using System.Collections.Generic;
using QFramework;
using QFramework.Example;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cheese : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IController
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    [SerializeField]
    private UICheesePanel UIParent;
    public bool isDraggable = false;
    
    public Texture2D cheeseTexture;      // 记得在导入设置开启 Read/Write Enabled
    
    public IArchitecture GetArchitecture()
    {
        return Global.Interface;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        // 自动获取父级 Canvas，用于计算缩放
        canvas = UIRoot.Instance.GetComponent<Canvas>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            // 拖拽时降低透明度，并允许射线穿透（这样才能检测到下方的 Drop 目标）
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            // delta 是鼠标移动的增量，除以 canvas 的缩放系数以保证同步
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            // 恢复透明度和射线检测
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;

            // 如果没有掉落在目标区域，可以考虑在这里写回弹逻辑
            var wordsInHoles = GetVisibleWords();
            this.SendCommand(new DropCheeseCommand(wordsInHoles));
        }
    }

    public List<CheeseWord> GetVisibleWords()
    {
        List<CheeseWord> visibleWords = new List<CheeseWord>();
        // 找到所有的单词物体
        Transform[] allWords = UIParent.Content.gameObject.GetComponentsInChildren<Transform>(false);
        
        foreach (Transform word in allWords)
        {
            // 获取单词的中心点（世界坐标）
            Vector3 wordCenter = word.position;

            if (IsPointInCheeseHole(wordCenter))
            {
                if (word.gameObject.TryGetComponent<CheeseWord>(out var comp))
                {
                    visibleWords.Add(comp);
                }
            }
        }

        return visibleWords;
    }

    private bool IsPointInCheeseHole(Vector3 worldPoint)
    {
        // 1. 将单词的世界中心点转换为 Cheese 的本地坐标
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform, 
                RectTransformUtility.WorldToScreenPoint(null, worldPoint), 
                null, 
                out localPoint))
        {
            return false;
        }

        // 2. 映射到 UV 空间 (0 到 1)
        // RectTransform 的 rect.x 是左下角相对于 Pivot 的偏移
        float u = (localPoint.x - rectTransform.rect.x) / rectTransform.rect.width;
        float v = (localPoint.y - rectTransform.rect.y) / rectTransform.rect.height;

        // 3. 边界检查：如果不在矩形内，直接排除
        if (u < 0 || u > 1 || v < 0 || v > 1) return false;

        // 4. 采样 Alpha
        int x = (int)(u * cheeseTexture.width);
        int y = (int)(v * cheeseTexture.height);
        
        float alpha = cheeseTexture.GetPixel(x, y).a;

        // 5. Alpha 为 0 表示全透明（即：洞）
        return alpha < 0.1f;
    }
}
