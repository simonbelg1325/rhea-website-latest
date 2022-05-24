import commonjs from '@rollup/plugin-commonjs';
import nodeResolve from '@rollup/plugin-node-resolve';
import vulcanize from './rollup.vulcanize.js';

export default [
	{
		input: 'wwwroot/web-components/app/app.js',
		plugins: [
			nodeResolve(),
			commonjs(),
			vulcanize()
		],
		output: [{ file: 'wwwroot/index.js' }],
		watch: {
			chokidar: {
			  usePolling: true,
			  interval: 5000
			}
		}
	}
];