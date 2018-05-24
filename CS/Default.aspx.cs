using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data.Filtering;
using DevExpress.Web;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ASPxCardView1_CustomCallback(object sender, DevExpress.Web.ASPxCardViewCustomCallbackEventArgs e)
    {
        ASPxCardView grid = sender as ASPxCardView;
        ModifyFilterExpression(ASPxComboBox1.Value.ToString(), ASPxTextBox1.Value, grid);
    }
    protected void ModifyFilterExpression(string FieldName, object value, ASPxCardView victim)
    {
        var criterias = CriteriaColumnAffinityResolver.SplitByColumnNames(CriteriaOperator.Parse(victim.FilterExpression)).Item2;

        BinaryOperatorType operatorType;
        if (FieldName == "ProductName")
        {
            operatorType = BinaryOperatorType.Like;
            value += "%";
        }
        else
            operatorType = BinaryOperatorType.Equal;


        if (!criterias.Keys.Contains(FieldName))
            criterias.Add(FieldName, new BinaryOperator(FieldName, value, operatorType));
        else
            criterias[FieldName] = new BinaryOperator(FieldName, value, operatorType);
        victim.FilterExpression = CriteriaOperator.ToString(GroupOperator.And(criterias.Values));
    }
}