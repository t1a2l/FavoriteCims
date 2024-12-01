using AlgernonCommons.UI;
using UnityEngine;

namespace FavoriteCims.UI.PanelsRows
{
    public class MultiTypeRow : UIListRow
    {
        private TitleRow _cachedTitleRow;
        private BuildingCitizenRow _cachedCitizenRow;
        private object _cachedObject;
        public override void Awake()
        {
            base.Awake();
            BackgroundOpacity = 0f;//We don't need background for this "row".
        }
        public override void Display(object data, int rowIndex)
        {
            if (_cachedTitleRow == null)
            {
                _cachedTitleRow = AddUIComponent<TitleRow>();
                _cachedTitleRow.height = _cachedTitleRow.RowHeight;
                _cachedTitleRow.width = width;
                _cachedTitleRow.relativePosition = new Vector3(0f, 0f);
                _cachedTitleRow.enabled = false;
            }
            if (_cachedCitizenRow == null)
            {
                _cachedCitizenRow = AddUIComponent<BuildingCitizenRow>();
                _cachedCitizenRow.height = _cachedTitleRow.RowHeight;
                _cachedCitizenRow.width = width;
                _cachedCitizenRow.relativePosition = new Vector3(0f, 0f);
                _cachedCitizenRow.enabled = false;
            }

            if (data is not null)
            {
                if (data is uint)
                {
                    _cachedCitizenRow.Display(data, rowIndex);
                    _cachedCitizenRow.Show();
                    _cachedCitizenRow.enabled = true;
                    _cachedTitleRow.Hide();
                    _cachedTitleRow.enabled = false;
                }
                else
                {
                    _cachedTitleRow.Display(data, rowIndex);
                    _cachedTitleRow.Show();
                    _cachedTitleRow.enabled = true;
                    _cachedCitizenRow.Hide();
                    _cachedCitizenRow.enabled = false;
                }
                _cachedObject = data;
                Deselect(rowIndex);
            }
        }

        public override void Select() { }
        public override void Deselect(int rowIndex)
        {
            if (_cachedTitleRow is not null)
                if (_cachedObject is uint)
                    _cachedCitizenRow?.Deselect(rowIndex);
                else _cachedTitleRow?.Deselect(rowIndex);
        }
    }
}
