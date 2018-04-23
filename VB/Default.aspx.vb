Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Data.Filtering
Imports DevExpress.Web

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub
    Protected Sub ASPxCardView1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxCardViewCustomCallbackEventArgs)
        Dim grid As ASPxCardView = TryCast(sender, ASPxCardView)
        ModifyFilterExpression(ASPxComboBox1.Value.ToString(), ASPxTextBox1.Value, grid)
    End Sub
    Protected Sub ModifyFilterExpression(ByVal FieldName As String, ByVal value As Object, ByVal victim As ASPxCardView)
        Dim criterias = CriteriaColumnAffinityResolver.SplitByColumns(CriteriaOperator.Parse(victim.FilterExpression))

        Dim operatorType As BinaryOperatorType
        If FieldName = "ProductName" Then
            operatorType = BinaryOperatorType.Like
            value &= "%"
        Else
            operatorType = BinaryOperatorType.Equal
        End If


        If Not criterias.Keys.Contains(New OperandProperty(FieldName)) Then
            criterias.Add(New OperandProperty(FieldName), New BinaryOperator(FieldName, value, operatorType))
        Else
            criterias(New OperandProperty(FieldName)) = New BinaryOperator(FieldName, value, operatorType)
        End If
        victim.FilterExpression = CriteriaOperator.ToString(GroupOperator.And(criterias.Values))
    End Sub
End Class