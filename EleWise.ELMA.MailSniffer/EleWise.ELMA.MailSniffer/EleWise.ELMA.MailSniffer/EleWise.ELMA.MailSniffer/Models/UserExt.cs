namespace EleWise.ELMA.MailSniffer.Models
{
    using System;
    using System.Linq;
    using EleWise.ELMA.Extensions;
    using Iesi.Collections.Generic;
    
    
    /// <summary>
    /// Пользователь расширенный
    /// </summary>
    [global::EleWise.ELMA.Model.Attributes.MetadataType(typeof(global::EleWise.ELMA.Model.Metadata.EntityMetadata))]
    [global::EleWise.ELMA.Model.Attributes.Uid("1a14f4a1-3b58-4c20-b4eb-2c5b5b42accc")]
    [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IUserExt), "DisplayName")]
    [global::EleWise.ELMA.Model.Attributes.BaseClass("cfdeb03c-35e9-45e7-aad8-037d83888f73")]
    [global::EleWise.ELMA.Model.Attributes.DisplayFormat(null)]
    [global::EleWise.ELMA.Model.Attributes.TableView(@"<?xml version=""1.0"" encoding=""utf-8""?>
<TableView xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <Uid>6b9785a2-0e1b-4002-b008-3a354ba3dd53</Uid>
  <ViewType>List</ViewType>
  <SortDescriptors />
  <GroupDescriptors />
</TableView>")]
    [global::EleWise.ELMA.Model.Attributes.Entity("UserExt")]
    [global::EleWise.ELMA.Model.Attributes.IdType("d90a59af-7e47-48c5-8c4c-dad04834e6e3")]
    [global::EleWise.ELMA.Model.Attributes.EntityMetadataType(global::EleWise.ELMA.Model.Metadata.EntityMetadataType.InterfaceExtension)]
    [global::EleWise.ELMA.Model.Attributes.ActionsType(typeof(UserExtActions))]
    public partial interface IUserExt : global::EleWise.ELMA.Security.Models.IUser
    {
        
        /// <summary>
        /// IP-адрес
        /// </summary>
        [global::EleWise.ELMA.Model.Attributes.Uid("b1369c0f-7bb9-40aa-a752-251819491739")]
        [global::EleWise.ELMA.Model.Attributes.Property("9b9aac17-22bb-425c-aa93-9c02c5146965")]
        [global::EleWise.ELMA.Model.Types.Settings.StringSettings(FieldName="IPAdress")]
        [global::EleWise.ELMA.Model.Attributes.DisplayName(typeof(@__Resources_IUserExt), "P_IPAdress_DisplayName")]
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
    }
    
    internal class @__Resources_IUserExt
    {
        
        public static string DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("Пользователь расширенный");
            }
        }
        
        public static string P_IPAdress_DisplayName
        {
            get
            {
                return global::EleWise.ELMA.SR.T("IP-адрес");
            }
        }
    }
}
