using Entidad_BE;
using Negocio_BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TP_SanchezVillaverde
{
    public partial class frmGestionPerfiles : Form
    {
        PerfilBLL permisoBLL;
        BitacoraBLL bitacoraBLL = new BitacoraBLL();
        string PathFile = "GestionPerfiles";

        public frmGestionPerfiles()
        {
            InitializeComponent();
            permisoBLL = new PerfilBLL();
        }

        public void CargarComboFamilias()
        {
            comboFamilias.Items.Clear();

            foreach (var p in permisoBLL.ListaPermisos("Compuesto"))
            {
                comboFamilias.Items.Add(p.getPermisoNombre());
            }
        }

        private void MostrarPermisos()
        {
            checkedListBox1.Items.Clear();
            foreach (Permiso P in permisoBLL.ListaPermisos(""))
            {
                checkedListBox1.Items.Add(P.getPermisoNombre());
            }
        }

        private void CargarArbol()
        {
            treeView1.Nodes.Clear();
            List<Permiso> PermisoRaiz = permisoBLL.ListaPermisosEnArbol();
            foreach (Permiso Pe in PermisoRaiz)
            {
                TreeNode tn = new TreeNode(Pe.getPermisoNombre());
                if (Pe is Familia familiaRaiz)
                {
                    if (familiaRaiz.EsRol)
                    {
                        tn.ForeColor = Color.Red; // Rojo para EsRol = true
                    }
                    else
                    {
                        tn.ForeColor = Color.Blue; // Azul para EsRol = false
                    }

                    LoadTreeRecursive(familiaRaiz, tn);
                }
                treeView1.Nodes.Add(tn);
            }
        }

        private void LoadTreeRecursive(Familia familiaActual, TreeNode parentNode)
        {
            foreach (var P in familiaActual.RetornarListaHijos())
            {
                TreeNode permisoHijo = new TreeNode(P.getPermisoNombre());
                if (P is Familia familiaHijo)
                {
                    if (familiaHijo.EsRol)
                    {
                        permisoHijo.ForeColor = Color.Red; // Rojo para EsRol = true
                    }
                    else
                    {
                        permisoHijo.ForeColor = Color.Blue; // Azul para EsRol = false
                    }

                    LoadTreeRecursive(familiaHijo, permisoHijo);
                }
                parentNode.Nodes.Add(permisoHijo);
            }
        }

        private void frmGestionPerfiles_Load(object sender, EventArgs e)
        {
            CargarArbol();
            MostrarPermisos();
            CargarComboFamilias();
            btnEliminar.Enabled = false;
            button7.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e) // Agregar familia
        {
            try
            {
                if (permisoBLL.ValidarNombre(textBox2.Text))
                    throw new Exception("Nombre repetido");

                List<string> items = new List<string>();
                foreach (var CI in checkedListBox1.CheckedItems)
                {
                    items.Add(CI.ToString());
                }

                var familiasseleccionadas = checkedListBox1.CheckedItems.Cast<string>().ToList();
                foreach (var fam in familiasseleccionadas)
                {
                    List<Permiso> listaux = permisoBLL.ObtenerHijosDeFamilia(fam.ToString());
                    List<string> familiapermisos = listaux.Select(p => p.Nombre).ToList();

                    foreach (string pnombre in items)
                    {
                        if (familiapermisos.Contains(pnombre))
                        {
                            throw new Exception($"Permiso '{pnombre}' ya está asignado a '{fam}'");
                        }
                    }
                }
                if ((items.Count + familiasseleccionadas.Count) <= 1)
                {
                    MessageBox.Show("Debe seleccionar permisos o familias");
                    return;
                }

                Familia auxFamlia;
                if (rBFamilia.Checked)
                {
                    auxFamlia = new Familia(textBox2.Text, false);
                }
                else
                {
                    auxFamlia = new Familia(textBox2.Text, true);
                }

                permisoBLL.AgregarFamilia(auxFamlia);
                permisoBLL.AgregarPermisoFamilia(textBox2.Text, items);
                bitacoraBLL.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.AltaUsuario);

                CargarArbol();
                MostrarPermisos();
                CargarComboFamilias();
                textBox2.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
        }

        private bool VerificarReferenciaCircular(Familia familiaBase, List<string> familiasagregadas)
        {
            foreach (var nombreFam in familiasagregadas)
            {
                var familiaAgregar = permisoBLL.ListaPermisosEnArbol().FirstOrDefault(f => f.Nombre == nombreFam) as Familia;

                if (familiaAgregar != null && VerificarReferenciaCircularRecursivo(familiaBase, familiaAgregar))
                {
                    return true;
                }
            }
            return false;
        }

        private bool VerificarReferenciaCircularRecursivo(Familia familiaBase, Familia familiaAgregar)
        {
            if (familiaBase.Nombre == familiaAgregar.Nombre)
            {
                return true;
            }
            foreach (var hijo in familiaAgregar.RetornarListaHijos())
            {
                if (hijo is Familia hijoFamilia)
                {
                    if (VerificarReferenciaCircularRecursivo(familiaBase, hijoFamilia))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void button7_Click(object sender, EventArgs e) //Modificar familia
        {
            try
            {
                Familia auxF = new Familia(comboFamilias.SelectedItem.ToString(), false);
                List<string> items = new List<string>();
                foreach (var CI in checkedListBox1.CheckedItems)
                {
                    items.Add(CI.ToString());
                }

                if (VerificarReferenciaCircular(permisoBLL.ListaPermisosEnArbol().Find(x => x.Nombre == comboFamilias.SelectedItem.ToString()) as Familia, items))
                {
                    throw new Exception("Referencia circular detectada");
                }

                var familiasseleccionadas = checkedListBox1.CheckedItems.Cast<string>().ToList();
                foreach (var fam in familiasseleccionadas)
                {
                    List<Permiso> listaux = permisoBLL.ObtenerHijosDeFamilia(fam.ToString());
                    List<string> familiapermisos = listaux.Select(p => p.Nombre).ToList();

                    foreach (string pnombre in items)
                    {
                        if (familiapermisos.Contains(pnombre))
                        {
                            throw new Exception($"Permiso '{pnombre}' ya está asignado a '{fam}'");
                        }
                    }
                }
                if ((items.Count + familiasseleccionadas.Count) <= 1)
                {
                    MessageBox.Show("Debe seleccionar permisos o familias");
                    return;
                }

                permisoBLL.ModificarFamilia(auxF, items);
                bitacoraBLL.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.ModificacionUsuario);

                CargarArbol();
                MostrarPermisos();
                comboFamilias.SelectedIndex = -1;
                comboFamilias.Text = "";
                textBox2.Text = "";
                button5.Enabled = true;
                button7.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LimpiarChecklis()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        public void ChequearChecklist(Familia familia)
        {
            ChequearChecklistRecursivo(familia, false);
        }

        private void ChequearChecklistRecursivo(Familia familia, bool segundo)
        {
            foreach (var P in familia.RetornarListaHijos())
            {
                if (segundo)
                {
                    if (P is Familia)
                    {
                        int index = checkedListBox1.Items.IndexOf(P.getPermisoNombre());
                        if (index != -1)
                        {
                            checkedListBox1.SetItemChecked(index, true);
                        }
                    }
                }
                else
                {
                    int index = checkedListBox1.Items.IndexOf(P.getPermisoNombre());
                    if (index != -1)
                    {
                        checkedListBox1.SetItemChecked(index, true);
                    }
                }

                if (P is Familia)
                {
                    ChequearChecklistRecursivo((Familia)P, true);
                }
            }
        }

        private void comboFamilias_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LimpiarChecklis();

                if (comboFamilias.SelectedItem != null)
                {
                    List<Permiso> ListaPermisos = permisoBLL.ListaPermisosEnArbol();
                    Permiso seleccionado = ListaPermisos.Find(x => x.getPermisoNombre() == comboFamilias.SelectedItem.ToString());

                    ChequearChecklist((Familia)seleccionado);
                    btnEliminar.Enabled = true;
                    button5.Enabled = false;
                    button7.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool FamiliaContenida(string familiaAEliminar, Familia familiaActual)
        {
            // Recorrer la lista de hijos de la familia actual
            foreach (Permiso hijo in familiaActual.RetornarListaHijos())
            {
                // Si el hijo es la familia que se quiere eliminar, retornar true
                if (hijo is Familia && hijo.Nombre == familiaAEliminar)
                {
                    return true;
                }

                // Si el hijo es una familia, llamar recursivamente a la función
                if (hijo is Familia)
                {
                    if (FamiliaContenida(familiaAEliminar, (Familia)hijo))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboFamilias.SelectedItem != null)
                {
                    List<Familia> ListaFamilias = permisoBLL.ListaPermisosEnArbol().OfType<Familia>().ToList();

                    foreach (Familia familia in ListaFamilias)
                    {
                        if (FamiliaContenida(comboFamilias.SelectedItem.ToString(), familia))
                        {
                            throw new Exception("La familia está en uso por otra familia");
                        }
                    }

                    if (permisoBLL.PerfilEnUso(comboFamilias.SelectedItem.ToString()))
                    {
                        throw new Exception($"El perfil está en uso: {comboFamilias.SelectedItem}");
                    }

                    permisoBLL.EliminarFamilia(new Familia(comboFamilias.SelectedItem.ToString(), false));
                    bitacoraBLL.RegistrarBitacora(SessionManager.GetInstance.UsuarioActual().user, TipoAccion.BajaUsuario);

                    CargarArbol();
                    MostrarPermisos();
                    CargarComboFamilias();
                    comboFamilias.SelectedIndex = -1;
                    comboFamilias.Text = "";
                    button5.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArbol();
                MostrarPermisos();
                comboFamilias.SelectedIndex = -1;
                comboFamilias.Text = "";
                textBox2.Text = "";
                button5.Enabled = true;
                btnEliminar.Enabled = false;
                button7.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ActualizarIdioma()
        {
            // Placeholder para actualización de idiomas
            // Implementar según tu sistema de traducción
        }
    }
}
