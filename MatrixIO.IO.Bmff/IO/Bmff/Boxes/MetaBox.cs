﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MatrixIO.IO.Bmff.Boxes
{
    /// <summary>
    /// Meta Box ("meta")
    /// </summary>
    [Box("meta", "Meta Box")]
    public sealed class MetaBox : FullBox, ISuperBox
    {
        public MetaBox() : base() { }
        public MetaBox(byte version, uint flags = 0) : base(version, flags) { }
        public MetaBox(Stream stream) : base(stream) { }

        public IList<Box> Children { get; } = new List<Box>();

        public IEnumerator<Box> GetEnumerator() => Children.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.GetEnumerator();

        /* TODO: Support standard sub-boxes!
        public HandlerBox TheHandler
        {
            get
            {
                return (from c in _Children
                        where c is HandlerBox
                        select (HandlerBox)c).FirstOrDefault();
            }
        }
        public PrimaryItemBox PrimaryResource
        {
            get
            {
                return (from c in _Children
                        where c is PrimaryItemBox
                        select (PrimaryItemBox)c).FirstOrDefault();
            }
        }
        */
        public DataInformationBox FileLocations
        {
            get
            {
                return (from c in Children
                        where c is DataInformationBox
                        select (DataInformationBox)c).FirstOrDefault();
            }
        }
        /*
        public ItemLocationBox ItemLocations
        {
            get
            {
                return (from c in _Children
                        where c is ItemLocationBox
                        select (ItemLocationBox)c).FirstOrDefault();
            }
        }
        */
        public ItemProtectionBox Protections
        {
            get
            {
                return (from c in Children
                        where c is ItemProtectionBox
                        select (ItemProtectionBox)c).FirstOrDefault();
            }
        }
        /*
        public ItemInfoBox ItemInfos
        {
            get
            {
                return (from c in _Children
                        where c is ItemInfonBox
                        select (ItemInfoBox)c).FirstOrDefault();
            }
        }

        public IPMPControlBox IPMPControl
        {
            get
            {
                return (from c in _Children
                        where c is IPMPControlBox
                        select (IPMPControlBox)c).FirstOrDefault();
            }
        }
        */

        private Type[] StandardBoxes =
        {
            //typeof(HandlerBox),
            //typeof(PrimaryItemBox),
            typeof(DataInformationBox),
            //typeof(ItemLocationBox),
            typeof(ItemProtectionBox),
            //typeof(ItemInfoBox),
            //typeof(IPMPControlBox),
        };

        public IEnumerable<Box> OtherBoxes
        {
            get
            {
                return from c in Children
                       where !StandardBoxes.Contains(c.GetType())
                       select c;
            }
        }
    }
}