using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

using CirclePrefect.Dotnet;

namespace D2CharacterPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region GUI Events
        private void box_attr_dexterity_LostFocus(object sender, RoutedEventArgs e)
        {
            box_attr_dexterity.Text = (ClassAttr.Dexterity + Base.Dexterity + Attributes.Dexterity + Equipment.Dexterity).ToString();
        }
        private void box_attr_vitality_LostFocus(object sender, RoutedEventArgs e)
        {
            box_attr_vitality.Text = (ClassAttr.Vitality + Base.Vitality + Attributes.Vitality + Equipment.Vitality).ToString();
        }
        private void box_attr_energy_LostFocus(object sender, RoutedEventArgs e)
        {
            box_attr_energy.Text = (ClassAttr.Energy + Base.Energy + Attributes.Energy + Equipment.Energy).ToString();
        }
        private void box_attr_strength_LostFocus(object sender, RoutedEventArgs e)
        {
             box_attr_strength.Text = (ClassAttr.Strength + Base.Strength + Attributes.Strength + Equipment.Strength).ToString();
        }

        private void combo_gear_resist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_gear_charms.SelectedIndex = -1;
            combo_gear_fcr.SelectedIndex = -1;
            combo_gear_magic.SelectedIndex = -1;
        }
        private void combo_gear_magic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_gear_charms.SelectedIndex = -1;
            combo_gear_fcr.SelectedIndex = -1;
            combo_gear_resist.SelectedIndex = -1;
        }
        private void combo_gear_charms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_gear_fcr.SelectedIndex = -1;
            combo_gear_resist.SelectedIndex = -1;
            combo_gear_magic.SelectedIndex = -1;
        }
        private void combo_gear_fcr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            combo_gear_charms.SelectedIndex = -1;
            combo_gear_resist.SelectedIndex = -1;
            combo_gear_magic.SelectedIndex = -1;
        }
        private void button_x_boots_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_boots.Content);
            label_equip_boots.Content = "...";
        }

        private void button_x_amulet_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_amulet.Content);
            label_equip_amulet.Content = "...";
        }

        private void button_x_ring1_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_ring1.Content);
            label_equip_ring1.Content = "...";
        }

        private void button_x_ring2_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_ring2.Content);
            label_equip_ring2.Content = "...";
        }

        private void button_x_mainh_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_mainh.Content);
            label_equip_mainh.Content = "...";
        }

        private void button_x_offh_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_offh.Content);
            label_equip_offh.Content = "...";
        }

        private void button_x_charms_Click(object sender, RoutedEventArgs e)
        {
            if (combo_equip_charms.Items.Count > 0)
            { 
                HandleUnequipItem(combo_equip_charms.Text);
                combo_equip_charms.Items.Remove(combo_equip_charms.SelectedItem);
            }
        }

        private void button_x_belt_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_belt.Content);
            label_equip_belt.Content = "...";
        }

        private void button_x_gloves_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_gloves.Content);
            label_equip_gloves.Content = "...";
        }

        private void button_x_bodyarmor_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_bodyarmor.Content);
            label_equip_bodyarmor.Content = "...";
        }
        private void button_x_helm_Click(object sender, RoutedEventArgs e)
        {
            HandleUnequipItem((string)label_equip_helm.Content);
            label_equip_helm.Content = "...";
        }
        private void ClassCheck(object sender, RoutedEventArgs e)
        {
            classFlag = true;
        }
        private void box_attr_base_strength_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(box_attr_base_strength.Text, out int str))
            {
                Base.Strength = Math.Min(str, 500);
                UpdateStats();
            }
        }

        private void box_attr_base_dexterity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(box_attr_base_dexterity.Text, out int dex))
            {
                Base.Dexterity = Math.Min(dex, 500);
                UpdateStats();
            }
        }

        private void box_attr_base_vitality_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(box_attr_base_vitality.Text, out int vit))
            {
                Base.Vitality = Math.Min(vit, 500);
                UpdateStats();
            }
        }

        private void box_attr_base_energy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(box_attr_base_energy.Text, out int energy))
            {
                Base.Energy = Math.Min(energy, 500);
                UpdateStats();
            }
        }
         private void Charms_Click(object sender, RoutedEventArgs e)
        {
            if (combo_gear_charms.SelectedIndex != -1)
            { 
                if (DB.BlockExists(combo_gear_charms.Text.Replace(" ", "").ToLower(), out Block item))
                {
                    item.WriteValue(active, "False");
                }
                combo_gear_charms.Items.Remove(combo_gear_charms.SelectedItem);
            }
        }

        private void MagicFind_Click(object sender, RoutedEventArgs e)
        {
            if (combo_gear_magic.SelectedIndex != -1)
            {
                if (DB.BlockExists(combo_gear_magic.Text.Replace(" ", "").ToLower(), out Block item))
                {
                    item.WriteValue(active, "False");
                }
                combo_gear_magic.Items.Remove(combo_gear_magic.SelectedItem);
            }
        }

        private void ResistButton_Click(object sender, RoutedEventArgs e)
        {
            if (combo_gear_resist.SelectedIndex != -1)
            {
                if (DB.BlockExists(combo_gear_resist.Text.Replace(" ", "").ToLower(), out Block item))
                {
                    item.WriteValue(active, "False");
                }
                combo_gear_resist.Items.Remove(combo_gear_resist.SelectedItem);
            }
        }

        private void FcrButton_Click(object sender, RoutedEventArgs e)
        {
            if (combo_gear_fcr.SelectedIndex != -1)
            {
                if (DB.BlockExists(combo_gear_fcr.Text.Replace(" ", "").ToLower(), out Block item))
                {
                    item.WriteValue(active, "False");
                }
                combo_gear_fcr.Items.Remove(combo_gear_fcr.SelectedItem);
            }
        }
        #endregion
        #region Attributes
        class ClassAttr
        {
            public static int
                Strength, 
                Dexterity,
                Vitality,
                Energy;
        }
        class Base
        {
            public static int
                Strength, 
                Dexterity,
                Vitality,
                Energy;
        }
        class Equipment
        {
            public static int
                Strength, 
                Dexterity,
                Vitality,
                Energy,
                MagicFind,
                FCR;
            public static void Clear()
            {
                Strength = 0;
                Dexterity = 0;
                Vitality = 0;
                Energy = 0;
                MagicFind = 0;
                FCR = 0;
            }
        }
        class Attributes
        {
            public static int
                Strength, 
                Dexterity,
                Vitality,
                Energy;
            public static void Clear()
            {
                Strength = 0;
                Dexterity = 0;
                Vitality = 0;
                Energy = 0;
            }
        }
        #endregion
        bool classFlag;
        internal static DataStore DB, CharDB;
        internal static List<Block> equipped = new List<Block>();
        const byte 
            Class_Assa = 0,
            Class_Amazon = 1,
            Class_Necro = 2,
            Class_Barb = 3,
            Class_Sorc = 4,
            Class_Druid = 5,
            Class_Paladin = 6,
            Gear_Helm = 0,
            Gear_BodyArmor = 1,
            Gear_Gloves = 2,
            Gear_Belt = 3,
            Gear_Boots = 4,
            Gear_Amulet = 5,
            Gear_Rings = 6,
            Gear_Weapons = 7,
            Gear_Shields = 8,
            Gear_Charms = 9;
        const string
            active = "active",
            name = "name",
            type = "type",
            strength = "str",
            dexterity = "dex",
            vitality = "vit",
            energy = "energy",
            magicFind = "magic",
            fcr = "fcr",
            req_strength = "req_str",
            req_dexterity = "req_dex",
            req_level = "req_level",
            type_helm = "helm",
            type_bodyarmor = "bodyarmor",
            type_gloves = "gloves",
            type_belt = "belt",
            type_boots = "boots",
            type_amulet = "amulet",
            type_ring = "ring",
            type_weapon = "weapon",
            type_shield = "shield",
            type_charm = "charm",
            charm_flag = "isCharm",
            fcr_flag = "isFcr",
            magic_flag = "isMagicFind",
            resist_flag = "isResist";
        string Type;
        readonly string[] values = new string[] 
        { 
            active,
            name,
            type,
            strength,
            dexterity,
            vitality,
            energy,
            magicFind,
            fcr,
            req_strength,
            req_dexterity,
            req_level,
            charm_flag,
            fcr_flag,
            magic_flag,
            resist_flag
        };
        const string
            helm = "helm",
            bodyarmor = "bodyarmor",
            gloves = "gloves",
            belt = "belt",
            boots = "boots",
            amulet = "amulet",
            ring1 = "ring1",
            ring2 = "ring2",
            mainh = "mainh",
            offh = "offh",
            charm = "charm";
        readonly string[] loadout = new string[]
        {
            active, 
            helm, 
            bodyarmor,
            gloves,
            belt,
            boots,
            amulet,
            ring1,
            ring2,
            mainh,
            offh,
            charm
        };
        
        private void RemoveLoadout_Click(object sender, RoutedEventArgs e)
        {
            string empty = "0";
            string Name = box_loadout_name.Text.Replace(" ", "").ToLower();
            if (CharDB.BlockExists(Name, out Block item))
            { 
                item.WriteValue(active, "False");
                item.WriteValue(helm, empty);
                item.WriteValue(bodyarmor, empty);
                item.WriteValue(gloves, empty);
                item.WriteValue(belt, empty);
                item.WriteValue(boots, empty);
                item.WriteValue(amulet, empty);
                item.WriteValue(ring1, empty);
                item.WriteValue(ring2, empty);
                item.WriteValue(mainh, empty);
                item.WriteValue(offh, empty);
                item.WriteValue(charm, empty);
                CharDB.WriteToFile();
            }  
        }

        private void SaveLoadout_Click(object sender, RoutedEventArgs e)
        {
            string Name = box_loadout_name.Text.Replace(" ", "").ToLower();
            if (CharDB.BlockExists(Name, out Block value) && value.GetValue(active) == "True" && MessageBox.Show("The loadout name is already registered in the equipment database. Do you want to overwrite it?", "Duplicate", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            { 
                Block item = CharDB.NewBlock(loadout, Name);
                item.WriteValue(active, "True");
                for (int i = 0; i < equipped.Count; i++)
                {
                    string dbName = equipped[i].GetValue(name);
                    switch (equipped[i].GetValue(type))
                    { 
                        case type_helm:
                            item.WriteValue(helm, dbName);
                            break;
                        case type_bodyarmor:
                            item.WriteValue(bodyarmor, dbName);
                            break;
                        case type_gloves:
                            item.WriteValue(gloves, dbName);
                            break;
                        case type_belt:
                            item.WriteValue(belt, dbName);
                            break;
                        case type_boots:
                            item.WriteValue(boots, dbName);
                            break;
                        case type_amulet:
                            item.WriteValue(amulet, dbName);
                            break;
                        case type_ring:
                            if ((string)label_equip_ring1.Content != "...")
                            { 
                                item.WriteValue(ring1, dbName);
                            }
                            if ((string)label_equip_ring2.Content != "...")
                            { 
                                item.WriteValue(ring2, dbName);
                            }
                            break;
                        case type_weapon:
                            if ((string)label_equip_mainh.Content != "...")
                            { 
                                item.WriteValue(mainh, dbName);
                            }
                            if (item.GetValue(offh) == "0" && (string)label_equip_offh.Content != "...")
                            { 
                                item.WriteValue(offh, dbName);
                            }
                            break;
                        case type_shield:
                            if (item.GetValue(offh) == "0" && (string)label_equip_offh.Content != "...")
                            { 
                                item.WriteValue(offh, dbName);
                            }
                            break;
                        case type_charm:
                            for (int n = 0; n < combo_equip_charms.Items.Count; n++)
                                item.AddValue(charm, ',', combo_equip_charms.Items[n].ToString());
                            break;
                    }
                }
                CharDB.WriteToFile();
            }
        }
        
        private void LoadLoadout_Click(object sender, RoutedEventArgs e)
        {
            string dbName = box_loadout_name.Text.Replace(" ", "").ToLower();
            string empty = "0", none = "...";
            if (CharDB.BlockExists(dbName, out Block item) && item.GetValue(active) == "True")
            {
                equipped.Clear();
                string name = "";
                Block value = default(Block);
                if ((name = item.GetValue(helm)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_helm.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_helm.Content = none;
                }
                if ((name = item.GetValue(bodyarmor)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_bodyarmor.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_bodyarmor.Content = none;
                }
                if ((name = item.GetValue(gloves)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_gloves.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_gloves.Content = none;
                }
                if ((name = item.GetValue(belt)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_belt.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_belt.Content = none;
                }
                if ((name = item.GetValue(boots)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_boots.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_boots.Content = none;
                }
                if ((name = item.GetValue(amulet)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_amulet.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_amulet.Content = none;
                }
                if ((name = item.GetValue(ring1)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_ring1.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_ring1.Content = none;
                }
                if ((name = item.GetValue(ring2)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_ring2.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_ring2.Content = none;
                }
                if ((name = item.GetValue(mainh)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_mainh.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_mainh.Content = none;
                }
                if ((name = item.GetValue(offh)) != empty && DB.BlockExists(name.ToLower(), out value))
                {
                    label_equip_offh.Content = name;
                    equipped.Add(value);
                }
                else
                {
                    label_equip_offh.Content = none;
                }
                if ((name = item.GetValue(charm)) != empty)
                {
                    string[] items = name.Split(',');
                    for (int i = 0; i < items.Length; i++)
                    { 
                        if (DB.BlockExists(items[i], out value))
                        { 
                            equipped.Add(value);
                        }
                    }
                }
                HandleEquipItem();
            }
            else
            {
                MessageBox.Show("Loadout: " + box_loadout_name.Text + " does not exist or is deactivated.", "Not present", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void HandleUnequipItem(string name)
        {
            if (DB.BlockExists(name.Replace(" ", "").ToLower(), out Block item))
            { 
                if (equipped.Contains(item))
                { 
                    equipped.Remove(item);
                    HandleEquipItem();
                }
            }
        }

        private void AddGear_Click(object sender, RoutedEventArgs e)
        {
            string name = "";
            string dbName = (name = box_gear_name.Text).Replace(" ", "").ToLower();
            if (DB.BlockExists(dbName) && DB.GetBlock(dbName).GetValue(active) == "False")
            {
                DB.GetBlock(dbName).WriteValue(active, "True");
            }
            else if (!DB.BlockExists(dbName))
            {
                string text = "False";
                if (checkbox_charm.IsChecked.Value)
                    text = "True";
                DB.NewBlock(values, new string[] 
                { 
                    "True",
                    name,
                    Type,
                    box_gear_str.Text,
                    box_gear_dex.Text,
                    box_gear_vit.Text,
                    box_gear_energy.Text,
                    box_gear_magic.Text,
                    box_gear_fcr.Text,
                    box_gear_req_str.Text,
                    box_gear_req_dex.Text,
                    box_gear_req_level.Text,
                    text,
                    (box_gear_fcr.Text != "0" && !string.IsNullOrWhiteSpace(box_gear_fcr.Text)).ToString(),
                    (box_gear_magic.Text != "0" && !string.IsNullOrWhiteSpace(box_gear_magic.Text)).ToString(),
                    "False"
                }, dbName);
                DB.WriteToFile();
            }
            else
            {
                MessageBox.Show("Database already contains item: " + name + ".", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        private void EquipItem_Click(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (combo_gear_fcr.SelectedIndex != -1)
                name = combo_gear_fcr.Text;
            else if (combo_gear_resist.SelectedIndex != -1)
                name = combo_gear_resist.Text;
            else if (combo_gear_magic.SelectedIndex != -1)
                name = combo_gear_magic.Text;
            else if (combo_gear_charms.SelectedIndex != -1)
                name = combo_gear_charms.Text;
            else
            { 
                MessageBox.Show("There is no item selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string empty = "...";
            if (DB.BlockExists(name.Replace(" ", "").ToLower(), out Block item))
            { 
                switch (item.GetValue(type))
                {
                    case type_helm:
                        if ((string)label_equip_helm.Content == empty)
                        { 
                            label_equip_helm.Content = name;
                            goto default;
                        }
                        break;
                    case type_bodyarmor:
                        if ((string)label_equip_bodyarmor.Content == empty)
                        { 
                            label_equip_bodyarmor.Content = name;
                            goto default;
                        }
                        break;
                    case type_gloves:
                        if ((string)label_equip_gloves.Content == empty)
                        { 
                            label_equip_gloves.Content = name;
                            goto default;
                        }
                        break;
                    case type_belt:
                        if ((string)label_equip_belt.Content == empty)
                        { 
                            label_equip_belt.Content = name;
                            goto default;
                        }
                        break;
                    case type_boots:
                        if ((string)label_equip_boots.Content == empty)
                        { 
                            label_equip_boots.Content = name;
                            goto default;
                        }
                        break;
                    case type_amulet:
                        if ((string)label_equip_amulet.Content == empty)
                        { 
                            label_equip_amulet.Content = name;
                            goto default;
                        }
                        break;
                    case type_ring:
                        if ((string)label_equip_ring1.Content == "...")
                        { 
                            label_equip_ring1.Content = name;
                            goto default;
                        }
                        else if ((string)label_equip_ring2.Content == "...")
                        { 
                            label_equip_ring2.Content = name;
                            goto default;
                        }
                        break;
                    case type_weapon:
                        if ((string)label_equip_mainh.Content == "...")
                        { 
                            label_equip_mainh.Content = name;
                            goto default;
                        }
                        else if ((string)label_equip_offh.Content == "...")
                        { 
                            label_equip_offh.Content = name;
                            goto default;
                        }
                        break;
                    case type_shield:
                        if ((string)label_equip_offh.Content == empty)
                        { 
                            label_equip_offh.Content = name;
                            goto default;
                        }
                        break;
                    case type_charm:
                        combo_equip_charms.Items.Add(name);
                        goto default;
                    default:
                        equipped.Add(item);
                        HandleEquipItem();
                        break;
                }
            }
            else
            {
                MessageBox.Show("This item is not in the database.", "Missing item", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HandleEquipItem()
        {
            int req_str = 0;
            int req_dex = 0;
            int req_lvl = 0;
            int item_str = 0;
            int item_dex = 0;
            int item_level = 0;
            int item_vit = 0;
            int item_energy = 0;
            int item_magic = 0;
            int item_fcr = 0;
            
            Equipment.Clear();
            box_stats_str.Text = "0";
            box_stats_dex.Text = "0";
            box_stats_level.Text = "0";

            for (int i = 0; i < equipped.Count; i++)
            {
                Block item = equipped[i];
                bool flag2 = int.TryParse(box_stats_str.Text, out req_str) &&
                             int.TryParse(box_stats_dex.Text, out req_dex) &&
                             int.TryParse(box_stats_level.Text, out req_lvl) &&
                             int.TryParse(item.GetValue(req_strength), out item_str) &&
                             int.TryParse(item.GetValue(req_dexterity), out item_dex) &&
                             int.TryParse(item.GetValue(req_level), out item_level);
                if (flag2)
                {
                    if (req_str < item_str)
                        box_stats_str.Text = item_str.ToString();
                    if (req_dex < item_dex)
                        box_stats_dex.Text = item_dex.ToString();
                    if (req_lvl < item_level)
                        box_stats_level.Text = item_level.ToString();
                }
            
                bool flag3 = int.TryParse(item.GetValue(strength), out item_str) &&
                             int.TryParse(item.GetValue(dexterity), out item_dex) &&
                             int.TryParse(item.GetValue(vitality), out item_vit) &&
                             int.TryParse(item.GetValue(energy), out item_energy) &&
                             int.TryParse(item.GetValue(magicFind), out item_magic) &&
                             int.TryParse(item.GetValue(fcr), out item_fcr);
                if (flag3)
                {
                    Equipment.Strength += item_str;
                    Equipment.Dexterity += item_dex;
                    Equipment.Vitality += item_vit;
                    Equipment.Energy += item_energy;
                    Equipment.MagicFind += item_magic;
                    Equipment.FCR += item_fcr;
                }
            }
            UpdateStats();
        }

        private void gear_helm_Checked(object sender, RoutedEventArgs e)
        {
            flag = true;
        }

        internal bool[] classSelect
        { 
            get 
            { 
                return new bool[]
                {
                    class_assa.IsChecked.Value,
                    class_amazon.IsChecked.Value,
                    class_necro.IsChecked.Value,
                    class_barb.IsChecked.Value,
                    class_sorc.IsChecked.Value,
                    class_druid.IsChecked.Value,
                    class_paladin.IsChecked.Value
                };
            }
        }
        internal bool[] gearSelect
        {
            get 
            { 
                return new bool[]
                {
                    gear_helm.IsChecked.Value,
                    gear_bodyarmor.IsChecked.Value,
                    gear_gloves.IsChecked.Value,
                    gear_belt.IsChecked.Value,
                    gear_boots.IsChecked.Value,
                    gear_amulet.IsChecked.Value,
                    gear_rings.IsChecked.Value,
                    gear_weapons.IsChecked.Value,
                    gear_shields.IsChecked.Value,
                    gear_charms.IsChecked.Value
                };
            }
        }
        private void UpdateStats()
        {
            box_attr_strength_LostFocus(null, null);
            box_attr_dexterity_LostFocus(null, null);
            box_attr_vitality_LostFocus(null, null);
            box_attr_energy_LostFocus(null, null);
        }
        bool flag;
        public MainWindow()
        {
            InitializeComponent();
            DB = new DataStore("gear");
            CharDB = new DataStore("loadout");
            Action method = null;
            method = new Action(() => 
            { 
                for (int i = 0; i < classSelect.Length; i++)
                {
                    if (classSelect[i])
                    { 
                        switch ((byte)i)
                        {
                            case Class_Assa:
                                ClassAttr.Strength = 20;
                                ClassAttr.Dexterity = 20;
                                ClassAttr.Vitality = 20;
                                ClassAttr.Energy = 25;
                                break;
                            case Class_Amazon:
                                ClassAttr.Strength = 20; 
                                ClassAttr.Dexterity = 25;
                                ClassAttr.Vitality = 20;
                                ClassAttr.Energy = 15;
                                break;
                            case Class_Necro:
                                ClassAttr.Strength = 15; 
                                ClassAttr.Dexterity = 25;
                                ClassAttr.Vitality = 15;
                                ClassAttr.Energy = 25;
                                break;
                            case Class_Barb:
                                ClassAttr.Strength = 30; 
                                ClassAttr.Dexterity = 20;
                                ClassAttr.Vitality = 25;
                                ClassAttr.Energy = 10;
                                break;
                            case Class_Sorc:
                                ClassAttr.Strength = 10; 
                                ClassAttr.Dexterity = 25;
                                ClassAttr.Vitality = 10;
                                ClassAttr.Energy = 35;
                                break;
                            case Class_Druid:
                                ClassAttr.Strength = 15; 
                                ClassAttr.Dexterity = 20;
                                ClassAttr.Vitality = 25;
                                ClassAttr.Energy = 20;
                                break;
                            case Class_Paladin:
                                ClassAttr.Strength = 25; 
                                ClassAttr.Dexterity = 20;
                                ClassAttr.Vitality = 25;
                                ClassAttr.Energy = 15;
                                break;
                        }
                    }
                }
                for (int i = 0; i < gearSelect.Length; i++)
                {
                    if (gearSelect[i])
                    { 
                        switch ((byte)i)
                        {
                            case Gear_Helm:
                                Type = "helm";
                                break;
                            case Gear_BodyArmor:
                                Type = "bodyarmor";
                                break;
                            case Gear_Gloves:
                                Type = "gloves";
                                break;
                            case Gear_Belt:
                                Type = "belt";
                                break;
                            case Gear_Boots:
                                Type = "boots";
                                break;
                            case Gear_Amulet:
                                Type = "amulet";
                                break;
                            case Gear_Rings:
                                Type = "ring";
                                break;
                            case Gear_Weapons:
                                Type = "weapon";
                                break;
                            case Gear_Shields:
                                Type = "shield";
                                break;
                            case Gear_Charms:
                                Type = "charm";
                                break;
                        }
                        break;
                    }
                }
                if (classFlag)
                {
                    UpdateStats();
                    classFlag = false;
                }
                if (flag)
                {
                    flag = false;
                    combo_gear_fcr.Items.Clear();
                    combo_gear_resist.Items.Clear();
                    combo_gear_magic.Items.Clear();
                    combo_gear_charms.Items.Clear();
                    for (int n = 0; n < DB.block.Length; n++)
                    {
                        if (DB.block[n] == default || DB.block[n].GetValue(type) != Type || DB.block[n].GetValue(active) == "False") 
                            continue;
                        if (bool.TryParse(DB.block[n].GetValue(charm_flag), out bool charm))
                        {
                            if (charm)
                            {
                                combo_gear_charms.Items.Add(DB.block[n].GetValue(name));
                            }
                        }
                        if (bool.TryParse(DB.block[n].GetValue(fcr_flag), out bool fcr))
                        { 
                            if (fcr) 
                            { 
                                combo_gear_fcr.Items.Add(DB.block[n].GetValue(name));
                            }
                        }
                        if (bool.TryParse(DB.block[n].GetValue(magic_flag), out bool magic))
                        {
                            if (magic)
                            {
                                combo_gear_magic.Items.Add(DB.block[n].GetValue(name));
                            }
                        }
                        if (bool.TryParse(DB.block[n].GetValue(resist_flag), out bool resist))
                        {
                            if (resist)
                            {
                                combo_gear_resist.Items.Add(DB.block[n].GetValue(name));
                            }
                        }
                    }
                }
                Dispatcher.BeginInvoke(method, DispatcherPriority.Background, null);
            });
            Dispatcher.BeginInvoke(method, DispatcherPriority.Background, null);
        }
    }
}
