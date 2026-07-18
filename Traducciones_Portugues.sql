-- =============================================================
-- T05: Diccionario completo en Portugues (PT) - 129 claves
-- Crea el idioma PT si no existe y recarga todas sus traducciones.
-- Es idempotente: se puede ejecutar las veces que haga falta.
--
-- Para rehacer la demo del alta en vivo (borrar PT y crearlo
-- desde frmIdiomas), ejecutar antes:
--   DELETE FROM Traduccion WHERE IdIdioma = (SELECT Id FROM Idioma WHERE Codigo = 'PT');
--   DELETE FROM Idioma WHERE Codigo = 'PT';
-- =============================================================

USE [FerreDB]
GO

IF NOT EXISTS (SELECT 1 FROM Idioma WHERE Codigo = 'PT')
    INSERT INTO Idioma (Codigo, Nombre) VALUES (N'PT', N'Português');
GO

DECLARE @PT INT = (SELECT Id FROM Idioma WHERE Codigo = 'PT');

DELETE FROM Traduccion WHERE IdIdioma = @PT;

INSERT INTO Traduccion (IdIdioma, Clave, Texto) VALUES
-- ------------------- Comunes -------------------
(@PT, N'COMUN_ATENCION',                N'Atenção'),
(@PT, N'COMUN_CONFIRMA_SALIR_APP',      N'Tem certeza de que deseja fechar o aplicativo?'),
(@PT, N'COMUN_CONFIRMA_CERRAR_SESION',  N'Tem certeza de que deseja encerrar a sessão?'),
(@PT, N'COMUN_ERROR_BD',                N'Erro de comunicação com o Banco de Dados: '),
(@PT, N'COMUN_SALIR',                   N'Sair'),
(@PT, N'COMUN_CANCELAR',                N'Cancelar'),
(@PT, N'COMUN_GUARDAR',                 N'Salvar'),
(@PT, N'COMUN_ACEPTAR',                 N'Aceitar'),
(@PT, N'COMUN_USUARIO',                 N'Usuário'),
(@PT, N'COMUN_CONTRASENA',              N'Senha'),
(@PT, N'COMUN_ROL',                     N'Função'),
(@PT, N'COMUN_NOMBRE',                  N'Nome'),
(@PT, N'COMUN_APELLIDO',                N'Sobrenome'),
(@PT, N'COMUN_FECHA',                   N'Data'),
(@PT, N'COMUN_ACCION',                  N'Ação'),
-- ------------------- Login -------------------
(@PT, N'LOGIN_TITULO',                  N'Login'),
(@PT, N'LOGIN_BTN_INICIAR',             N'Entrar'),
(@PT, N'LOGIN_LNK_SIN_CONEXION',        N'Iniciar sem conexão'),
(@PT, N'LOGIN_TIT_DATOS_INVALIDOS',     N'Dados Inválidos'),
(@PT, N'LOGIN_MSG_DATOS_INVALIDOS',     N'Os dados informados não atendem ao formato exigido.'),
(@PT, N'LOGIN_ERR_USER_INEXISTENTE',    N'O usuário informado não existe'),
(@PT, N'LOGIN_ERR_USER_BLOQUEADO',      N'O usuário está bloqueado. Contate o administrador'),
(@PT, N'LOGIN_ERR_PASS_INCORRECTA',     N'A senha informada está incorreta'),
(@PT, N'LOGIN_MSG_USER_INACTIVO',       N'O usuário -->{0}<-- não está disponível. Contate o administrador.'),
(@PT, N'LOGIN_MSG_FIN_INTENTOS',        N'Número de tentativas excedido, o usuário foi bloqueado. Fechando o aplicativo.'),
(@PT, N'LOGIN_MSG_SESION_INICIADA',     N'O usuário -->{0}<-- já possui uma sessão ativa.'),
(@PT, N'LOGIN_MSG_EXISTE_SESION',       N'Já existe uma sessão ativa, deseja encerrá-la?'),
(@PT, N'LOGIN_MSG_SESION_CERRADA',      N'Sessão encerrada com sucesso. Tente novamente com seu usuário'),
(@PT, N'LOGIN_MSG_USUARIO_EN_USO',      N'Tente novamente com o usuário atualmente em uso. Se não for o seu, por favor encerre a sessão'),
-- ------------------- Menu -------------------
(@PT, N'MENU_TS_INICIO',                N'Início'),
(@PT, N'MENU_INICIAR_SESION',           N'Entrar'),
(@PT, N'MENU_CERRAR_SESION',            N'Encerrar Sessão'),
(@PT, N'MENU_TS_ADMIN',                 N'Admin'),
(@PT, N'MENU_GESTION_USUARIOS',         N'Gestão de Usuários'),
(@PT, N'MENU_PERFILES',                 N'Perfis'),
(@PT, N'MENU_IDIOMAS',                  N'Idiomas'),
(@PT, N'MENU_BACKUP',                   N'Backup'),
(@PT, N'MENU_RESTORE',                  N'Restore'),
(@PT, N'MENU_BITACORA',                 N'Registro de Eventos'),
(@PT, N'MENU_CAMBIAR_CLAVE',            N'Alterar Senha'),
(@PT, N'MENU_CAMBIAR_IDIOMA',           N'Alterar Idioma'),
(@PT, N'MENU_TS_VENTAS',                N'Vendas'),
(@PT, N'MENU_LLENAR_CARRITO',           N'Encher Carrinho'),
(@PT, N'MENU_REALIZAR_VENTA',           N'Realizar Venda'),
(@PT, N'MENU_TS_GESTION',               N'Gestão'),
(@PT, N'MENU_CLIENTE',                  N'Cliente'),
(@PT, N'MENU_PRODUCTOS',                N'Produtos'),
(@PT, N'MENU_PROVEEDORES',              N'Fornecedores'),
(@PT, N'MENU_TS_REPORTES',              N'Relatórios'),
(@PT, N'MENU_TS_AYUDA',                 N'Ajuda'),
(@PT, N'MENU_LBL_USUARIO',              N'Usuário: '),
(@PT, N'MENU_LBL_SIN_CONEXION',         N'Usuário: Sem Conexão'),
-- ------------------- Gestion de Usuarios -------------------
(@PT, N'USU_TITULO',                    N'Gestão de Usuários'),
(@PT, N'USU_LBL_TITULO',                N'GESTÃO DE USUÁRIOS'),
(@PT, N'USU_LBL_OPCIONES',              N'Opções'),
(@PT, N'USU_GB_DATOS',                  N'Dados do Usuário'),
(@PT, N'USU_GB_MENSAJES',               N'Mensagens'),
(@PT, N'USU_LBL_DNI',                   N'Documento'),
(@PT, N'USU_LBL_DIRECCION',             N'Endereço'),
(@PT, N'USU_LBL_TELEFONO',              N'Telefone'),
(@PT, N'USU_LBL_EMAIL',                 N'E-mail'),
(@PT, N'USU_CHK_ACTIVO',                N'Ativo'),
(@PT, N'USU_CHK_BLOQUEADO',             N'Bloqueado'),
(@PT, N'USU_BTN_CREAR',                 N'Criar Usuário'),
(@PT, N'USU_BTN_MODIFICAR',             N'Editar Usuário'),
(@PT, N'USU_BTN_ELIMINAR',              N'Excluir Usuário'),
(@PT, N'USU_BTN_DESBLOQUEAR',           N'Desbloquear'),
(@PT, N'USU_MSG_DNI_EXISTE',            N'O documento informado já possui um usuário'),
(@PT, N'USU_MSG_USER_EXISTE',           N'O usuário informado já existe'),
(@PT, N'USU_MSG_CREADO',                N'O usuário -- {0} -- foi criado com sucesso'),
(@PT, N'USU_MSG_ACTUALIZADO',           N'Usuário -- {0} -- atualizado com sucesso'),
(@PT, N'USU_MSG_CONFIRMA_ELIMINAR',     N'Deseja excluir o usuário -- {0} -- ?'),
(@PT, N'USU_MSG_BAJA_OK',               N'Usuário -- {0} -- desativado com sucesso'),
(@PT, N'USU_MSG_CONFIRMA_DESBLOQUEO',   N'Deseja continuar com o desbloqueio?'),
(@PT, N'USU_TIT_DESBLOQUEAR',           N'Desbloquear Usuário'),
(@PT, N'USU_MSG_DESBLOQUEO_OK',         N'Usuário desbloqueado com sucesso'),
(@PT, N'USU_ERR_CREAR',                 N'Ocorreu um erro na criação do usuário: '),
(@PT, N'USU_ERR_ACTUALIZAR',            N'Ocorreu um erro na atualização do usuário: '),
(@PT, N'USU_ERR_ELIMINAR',              N'Não foi possível excluir o usuário selecionado: '),
(@PT, N'USU_ERR_ROLES',                 N'Erro ao carregar funções de Perfis: '),
(@PT, N'USU_VAL_NOMBRE',                N'Os campos Nome e/ou Sobrenome contêm caracteres inválidos. Digite apenas letras, por favor.'),
(@PT, N'USU_VAL_NUMEROS',               N'Os campos Documento e/ou Telefone contêm caracteres inválidos. Digite apenas números, por favor.'),
(@PT, N'USU_VAL_DIRECCION',             N'O campo Endereço contém caracteres inválidos. Digite apenas letras e números, por favor.'),
(@PT, N'USU_VAL_USUARIO',               N'O campo Usuário contém caracteres inválidos. Não são permitidos espaços nem caracteres especiais.'),
(@PT, N'USU_VAL_MAIL',                  N'O campo E-mail contém caracteres inválidos. Siga o formato xxxx@xxx.xxx, sem espaços nem caracteres especiais.'),
-- ------------------- Bitacora -------------------
(@PT, N'BIT_TITULO',                    N'Registro de Eventos'),
(@PT, N'BIT_LBL_TITULO',                N'EVENTOS'),
(@PT, N'BIT_COL_REGISTRO',              N'ID do Registro'),
-- ------------------- Cambio de Clave -------------------
(@PT, N'CLAVE_TITULO',                  N'Alterar Senha'),
(@PT, N'CLAVE_LBL_ACTUAL',              N'Senha Atual'),
(@PT, N'CLAVE_LBL_NUEVA',               N'Nova Senha'),
(@PT, N'CLAVE_LBL_REPETIR',             N'Repetir Senha'),
(@PT, N'CLAVE_BTN_CONFIRMAR',           N'Confirmar'),
(@PT, N'CLAVE_MSG_INSTRUCCIONES',       N'Digite uma senha diferente da anterior. Lembre-se de usar apenas letras e números'),
(@PT, N'CLAVE_ERR_INTENTOS',            N'Senha incorreta, restam {0} tentativas'),
(@PT, N'CLAVE_MSG_BLOQUEADO',           N'Número de tentativas excedido, usuário bloqueado. Contate o administrador'),
(@PT, N'CLAVE_TIT_BLOQUEADO',           N'Usuário Bloqueado'),
(@PT, N'CLAVE_MSG_OK',                  N'Senha alterada com sucesso'),
(@PT, N'CLAVE_TIT_OK',                  N'Nova Senha'),
(@PT, N'CLAVE_ERR_FORMATO',             N'Formato de senha informado incorreto'),
(@PT, N'CLAVE_ERR_DISTINTAS',           N'As senhas são diferentes, tente novamente'),
(@PT, N'CLAVE_ERR_IGUAL_ANTERIOR',      N'A nova senha deve ser diferente da anterior'),
(@PT, N'CLAVE_ERR_NO_FORMATO',          N'A senha não atende ao formato exigido'),
-- ------------------- Gestion de Perfiles -------------------
(@PT, N'PERF_TITULO',                   N'Gestão de Perfis'),
(@PT, N'PERF_LBL_TITULO',               N'GESTÃO DE PERFIS'),
(@PT, N'PERF_GB_DATOS',                 N'Dados do Perfil'),
(@PT, N'PERF_GB_PERMISOS',              N'Permissões e Famílias Disponíveis'),
(@PT, N'PERF_GB_ARBOL',                 N'Árvore de Hierarquia de Permissões'),
(@PT, N'PERF_LBL_NOMBRE',               N'Nome:'),
(@PT, N'PERF_LBL_ROL_FAMILIA',          N'Função / Família:'),
(@PT, N'PERF_RB_ROL',                   N'Função'),
(@PT, N'PERF_RB_FAMILIA',               N'Família'),
(@PT, N'PERF_BTN_CREAR',                N'Criar'),
(@PT, N'PERF_BTN_ELIMINAR',             N'Excluir'),
(@PT, N'PERF_MSG_SELECCION',            N'Você deve selecionar permissões ou famílias'),
-- ------------------- Gestion de Idiomas -------------------
(@PT, N'IDI_TITULO',                    N'Gestão de Idiomas'),
(@PT, N'IDI_GB_NUEVO',                  N'Novo Idioma'),
(@PT, N'IDI_LBL_CODIGO',                N'Código'),
(@PT, N'IDI_LBL_BASE',                  N'Ao criar um idioma, as traduções do idioma base (Espanhol) são copiadas como ponto de partida.'),
(@PT, N'IDI_BTN_CREAR',                 N'Criar Idioma'),
(@PT, N'IDI_GB_TRADUCCIONES',           N'Traduções'),
(@PT, N'IDI_LBL_IDIOMA',                N'Idioma:'),
(@PT, N'IDI_COL_CLAVE',                 N'Chave'),
(@PT, N'IDI_COL_TEXTO',                 N'Texto'),
(@PT, N'IDI_MSG_CREADO',                N'O idioma -- {0} -- foi criado com sucesso'),
(@PT, N'IDI_MSG_GUARDADO',              N'Traduções salvas com sucesso'),
(@PT, N'IDI_ERR_DATOS',                 N'Você deve informar o código e o nome do idioma'),
(@PT, N'IDI_ERR_LONGITUD',              N'O código permite até 5 caracteres e o nome até 50'),
(@PT, N'IDI_ERR_CODIGO_EXISTE',         N'Já existe um idioma com esse código'),
(@PT, N'IDI_LBL_ARCHIVO',               N'Arquivo de traduções (opcional)'),
(@PT, N'IDI_BTN_EXAMINAR',              N'Procurar...'),
(@PT, N'IDI_MSG_IMPORTADAS',            N'O idioma -- {0} -- foi criado com {1} traduções importadas'),
(@PT, N'IDI_ERR_ARCHIVO',               N'Não foi possível ler o arquivo de traduções: ');
GO
