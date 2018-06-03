namespace EleWise.ELMA.MailSniffer.Models
{
    using System;
    using System.Linq;
    using EleWise.ELMA.Extensions;
    using Iesi.Collections.Generic;
    
    
    /// <summary>
    /// Инцидент
    /// </summary>
    [global::EleWise.ELMA.Model.Attributes.MetadataType(typeof(global::EleWise.ELMA.Model.Metadata.EntityMetadata))]
    [global::EleWise.ELMA.Model.Attributes.Uid("65269d0d-b9c7-415a-b79b-64af6a212ebf")]
    [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "DisplayName")]
    [global::EleWise.ELMA.Model.Attributes.DisplayFormat(null)]
    [global::EleWise.ELMA.Model.Attributes.TitleProperty("9e56a98c-12f0-4e1b-aef0-6c800491d92c")]
    [global::EleWise.ELMA.Model.Attributes.ClassFormsScheme(global::EleWise.ELMA.Model.Metadata.ClassFormsScheme.FormConstructor)]
    [global::EleWise.ELMA.Model.Attributes.DefaultForm(global::EleWise.ELMA.Model.Views.ViewType.Create, "00000000-0000-0000-0000-000000000000", "29b3717d-bdf7-43db-8182-8b01e84da645", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", false)]
    [global::EleWise.ELMA.Model.Attributes.DefaultForm(global::EleWise.ELMA.Model.Views.ViewType.Edit, "00000000-0000-0000-0000-000000000000", "29b3717d-bdf7-43db-8182-8b01e84da645", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", false)]
    [global::EleWise.ELMA.Model.Attributes.DefaultForm(global::EleWise.ELMA.Model.Views.ViewType.Display, "00000000-0000-0000-0000-000000000000", "29b3717d-bdf7-43db-8182-8b01e84da645", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", false)]
    [global::EleWise.ELMA.Model.Attributes.Form(typeof(IIncident), "EleWise.ELMA.MailSniffer.Models.Incident.Form.Form.xml")]
    [global::EleWise.ELMA.Model.Attributes.TableView(@"<?xml version=""1.0"" encoding=""utf-8""?>
<TableView xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Uid>d20ae569-abf8-4688-bc62-373a99576012</Uid>
  <ViewType>List</ViewType>
  <SortDescriptors />
  <GroupDescriptors />
</TableView>")]
    [global::EleWise.ELMA.Model.Attributes.Entity("Incident")]
    [global::EleWise.ELMA.Model.Attributes.IdType("d90a59af-7e47-48c5-8c4c-dad04834e6e3")]
    [global::EleWise.ELMA.Model.Attributes.EntityMetadataType(global::EleWise.ELMA.Model.Metadata.EntityMetadataType.Interface)]
    [global::EleWise.ELMA.Model.Attributes.ShowInCatalogList()]
    [global::EleWise.ELMA.Model.Attributes.Filterable()]
    [global::EleWise.ELMA.Model.Attributes.ImplementationUid("5809150e-f092-46c2-9558-8c75e19ef6d6")]
    [global::EleWise.ELMA.Model.Attributes.FilterType(typeof(IIncidentFilter))]
    public partial interface IIncident : global::EleWise.ELMA.Model.Entities.IEntity<long>
    {
        
        /// <summary>
        /// UID
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("baddeb0a-60ec-4f9e-b809-6ad3b3009aa7")]
        [global::EleWise.ELMA.ComponentModel.NotNull()]
        [global::EleWise.ELMA.Model.Attributes.SystemProperty()]
        [global::EleWise.ELMA.Model.Attributes.Property("eb6e8ddc-fafe-4e0e-9018-1a7667012579")]
        [global::EleWise.ELMA.Model.Types.Settings.GuidSettings(FieldName="Uid")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_Uid_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.All, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        System.Guid Uid
        {
            get;
            set;
        }
        
        /// <summary>
        /// Пользователь
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("56f6f9cd-cb2f-40bb-a678-dbae83082ff1")]
        [global::EleWise.ELMA.Model.Attributes.Order(1)]
        [global::EleWise.ELMA.Model.Attributes.Property("72ed98ca-f260-4671-9bcd-ff1d80235f47", "cfdeb03c-35e9-45e7-aad8-037d83888f73")]
        [global::EleWise.ELMA.Security.Types.Settings.EntityUserSettings(CascadeMode=global::EleWise.ELMA.Model.Types.Settings.CascadeMode.SaveUpdate, FieldName="User")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_User_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.Filterable()]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        EleWise.ELMA.Security.Models.IUser User
        {
            get;
            set;
        }
        
        /// <summary>
        /// Статус
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("35034266-e8d2-4938-90b8-442b829c124c")]
        [global::EleWise.ELMA.Model.Attributes.Order(2)]
        [global::EleWise.ELMA.ComponentModel.NotNull()]
        [global::EleWise.ELMA.Model.Attributes.Property("849c1ac9-4d46-4194-8cbb-43f84adf9c17", "b3dc98b7-07a4-4a4a-9d51-912c59d2f52d")]
        [global::EleWise.ELMA.Model.Types.Settings.EnumSettings(RelationType=global::EleWise.ELMA.Model.Types.Settings.EnumRelationType.Many, FieldName="Status")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_Status_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.Filterable()]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        EleWise.ELMA.MailSniffer.Models.SniffState Status
        {
            get;
            set;
        }
        
        /// <summary>
        /// Наименование
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("9e56a98c-12f0-4e1b-aef0-6c800491d92c")]
        [global::EleWise.ELMA.Model.Attributes.Order(3)]
        [global::EleWise.ELMA.Model.Attributes.Property("9b9aac17-22bb-425c-aa93-9c02c5146965")]
        [global::EleWise.ELMA.Model.Types.Settings.StringSettings(FieldName="Name")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_Name_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        string Name
        {
            get;
            set;
        }
        
        /// <summary>
        /// IP адрес
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("8b8de9c4-9479-4ab3-9fa8-f47e82f2b1de")]
        [global::EleWise.ELMA.Model.Attributes.Order(4)]
        [global::EleWise.ELMA.Model.Attributes.Property("9b9aac17-22bb-425c-aa93-9c02c5146965")]
        [global::EleWise.ELMA.Model.Types.Settings.StringSettings(FieldName="IPAdress")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_IPAdress_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        string IPAdress
        {
            get;
            set;
        }
        
        /// <summary>
        /// Наименование файла
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("3cd5ca6c-5369-4c5e-a4e5-096cbf0da835")]
        [global::EleWise.ELMA.Model.Attributes.Order(5)]
        [global::EleWise.ELMA.Model.Attributes.Property("9b9aac17-22bb-425c-aa93-9c02c5146965")]
        [global::EleWise.ELMA.Model.Types.Settings.StringSettings(FieldName="FileName")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_FileName_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        string FileName
        {
            get;
            set;
        }
        
        /// <summary>
        /// Описание
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("45784e8e-725f-4bf5-a16e-82e0c8e1b031")]
        [global::EleWise.ELMA.Model.Attributes.Order(6)]
        [global::EleWise.ELMA.Model.Attributes.Property("9b9aac17-22bb-425c-aa93-9c02c5146965")]
        [global::EleWise.ELMA.Model.Types.Settings.StringSettings(MultiLine=true, FieldName="Description")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_Description_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        string Description
        {
            get;
            set;
        }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("c8b5b90d-7316-417a-b510-cb1e83fc04a2")]
        [global::EleWise.ELMA.Model.Attributes.Order(7)]
        [global::EleWise.ELMA.ComponentModel.CanBeNull()]
        [global::EleWise.ELMA.Model.Attributes.Property("dac9211e-e02b-47cd-8868-89a3bfc0f749")]
        [global::EleWise.ELMA.Model.Types.Settings.DateTimeSettings(FieldName="CreationDate")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_CreationDate_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        System.Nullable<System.DateTime> CreationDate
        {
            get;
            set;
        }
        
        /// <summary>
        /// Дата записи последнего инцидента
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("08e6d290-2758-4794-ac14-3869b4e70142")]
        [global::EleWise.ELMA.Model.Attributes.Order(8)]
        [global::EleWise.ELMA.ComponentModel.CanBeNull()]
        [global::EleWise.ELMA.Model.Attributes.Property("dac9211e-e02b-47cd-8868-89a3bfc0f749")]
        [global::EleWise.ELMA.Model.Types.Settings.DateTimeSettings(FieldName="LastIncidentDate")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_LastIncidentDate_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        System.Nullable<System.DateTime> LastIncidentDate
        {
            get;
            set;
        }
        
        /// <summary>
        /// Содержимое почтового потока
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("835c0923-3420-4a15-bf19-0ef26ad6b61f")]
        [global::EleWise.ELMA.Model.Attributes.Order(9)]
        [global::EleWise.ELMA.Model.Attributes.Property("72ed98ca-f260-4671-9bcd-ff1d80235f47", "3536c931-154c-4618-93b8-4e35bd8db226")]
        [global::EleWise.ELMA.Model.Types.Settings.EntitySettings(RelationType=global::EleWise.ELMA.Model.Types.Settings.RelationType.ManyToMany, RelationTableName="M_Incident_AttachmentList", ParentColumnName="Parent", ChildColumnName="Child", CascadeMode=global::EleWise.ELMA.Model.Types.Settings.CascadeMode.SaveUpdate)]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IIncident), "P_AttachmentList_DisplayName")]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Create, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Edit, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Display, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=true)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.List, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Hidden, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.View(ViewType=global::EleWise.ELMA.Model.Views.ViewType.Filter, ItemType=global::EleWise.ELMA.Model.Views.ItemType.Default, Visibility=global::EleWise.ELMA.Model.Views.Visibility.Visible, ReadOnly=false)]
        [global::EleWise.ELMA.Model.Attributes.EntityProperty()]
        Iesi.Collections.Generic.ISet<EleWise.ELMA.Common.Models.IAttachment> AttachmentList
        {
            get;
            set;
        }
    }
    
    internal class @__Resources_IIncident
    {
        
        public static string DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Инцидент");
            }
        }
        
        public static string P_Uid_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("UID");
            }
        }
        
        public static string P_User_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Пользователь");
            }
        }
        
        public static string P_Status_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Статус");
            }
        }
        
        public static string P_Name_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Наименование");
            }
        }
        
        public static string P_IPAdress_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("IP адрес");
            }
        }
        
        public static string P_FileName_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Наименование файла");
            }
        }
        
        public static string P_Description_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Описание");
            }
        }
        
        public static string P_CreationDate_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Дата создания");
            }
        }
        
        public static string P_LastIncidentDate_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Дата записи последнего инцидента");
            }
        }
        
        public static string P_AttachmentList_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Содержимое почтового потока");
            }
        }
        
        private static string @__AllFormsResources
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Edit");
            }
        }
    }
    
    /// <summary>
    /// Filter for the "Инцидент" object
    /// </summary>
    [global::EleWise.ELMA.Model.Attributes.FilterFor(typeof(IIncident))]
    public interface IIncidentFilter : global::EleWise.ELMA.Model.Common.IEntityFilter
    {
        
        /// <summary>
        /// Filter for the "Пользователь" property
        /// </summary>
        EleWise.ELMA.Security.Models.IUser User
        {
            get;
            set;
        }
        
        /// <summary>
        /// Filter for the "Статус" property
        /// </summary>
        System.Collections.Generic.List<EleWise.ELMA.MailSniffer.Models.SniffState> Status
        {
            get;
            set;
        }
    }
}
