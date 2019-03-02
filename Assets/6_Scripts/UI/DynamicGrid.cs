using UnityEngine;
using UnityEngine.UI;

public class DynamicGrid : MonoBehaviour {

	[SerializeField] private int col;
	private int row;

	private RectTransform parent;
	private GridLayoutGroup grid;

	// Use this for initialization
	void Start () {
		parent = gameObject.GetComponent<RectTransform>();
		grid = gameObject.GetComponent<GridLayoutGroup>();

		float size = (parent.rect.width / col) - grid.spacing.x;

		grid.cellSize = new Vector2(size, size);
	}
}
