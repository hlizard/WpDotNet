<?php
/**
 * The base configurations of the WordPress.
 *
 * This file has the following configurations: MySQL settings, Table Prefix,
 * Secret Keys, WordPress Language, and ABSPATH. You can find more information
 * by visiting {@link http://codex.wordpress.org/Editing_wp-config.php Editing
 * wp-config.php} Codex page. You can get the MySQL settings from your web host.
 *
 * This file is used by the wp-config.php creation script during the
 * installation. You don't have to use the web site, you can just copy this file
 * to "wp-config.php" and fill in the values.
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define('DB_NAME', 'wordpress');

/** MySQL database username */
define('DB_USER', 'root');

/** MySQL database password */
define('DB_PASSWORD', 'TODO');

/** MySQL hostname */
define('DB_HOST', 'localhost');

/** Database Charset to use in creating database tables. */
define('DB_CHARSET', 'utf8');

/** The Database Collate type. Don't change this if in doubt. */
define('DB_COLLATE', '');

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define('AUTH_KEY',         'nyf62Vther_dhkkg)qAKpHM)2W(E)+ -L!?)~*vNto-V2hDA-_PJNY!vn6*} baq');
define('SECURE_AUTH_KEY',  '#@[w4D0KC.wQBPOT875!$T@7+U/9?$5Dn&f+u,cowG)4a_I>fc(Z7~gNH1hmONEh');
define('LOGGED_IN_KEY',    '@8Z_-gTj9F,c@r`NZ!SQgoVQ0%9f -$ tVcWS#.[H(P(/4}Os$4~1h^5V:GG[KT[');
define('NONCE_KEY',        'o~#Gx=V#*dt8&Y,9Z5a%lpAwo_Fa9Cc<d|}W#a5H40Xwv(znIA/7/I;,Ww{XvLP.');
define('AUTH_SALT',        '[>`@>?e5g#)z{eKLjy1V5h%s!V4M{md8i4htJ*JRJ%#W&lZri+v wieUZlWDB&lT');
define('SECURE_AUTH_SALT', 'jBslgOxbLm`.[xdXIe=(POthhV?Us}gll-):@U,^7s^L6(7{3I3J,@{@MQWjJUma');
define('LOGGED_IN_SALT',   'IQovKTwo^dwrm?{/rzH9O);^f}~Mqps&-dR:ND`W`hWPy_a7~3M>em6F!!D+zM$!');
define('NONCE_SALT',       'e_`Lnc0{];,cnmSz`#wG{0[=aJ}$OIaXQ.^WH@?bmK*`qk(oLB(m%&Em_$(.J}+K');

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each a unique
 * prefix. Only numbers, letters, and underscores please!
 */
$table_prefix  = 'wp_';

/**
 * WordPress Localized Language, defaults to English.
 *
 * Change this to localize WordPress. A corresponding MO file for the chosen
 * language must be installed to wp-content/languages. For example, install
 * de_DE.mo to wp-content/languages and set WPLANG to 'de_DE' to enable German
 * language support.
 */
define('WPLANG', '');

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 */
define('WP_DEBUG', true);

/* That's all, stop editing! Happy blogging. */

/** Absolute path to the WordPress directory. */
if ( !defined('ABSPATH') )
	define('ABSPATH', dirname(__FILE__) . '/');

/** Sets up WordPress vars and included files. */
require_once(ABSPATH . 'wp-settings.php');
