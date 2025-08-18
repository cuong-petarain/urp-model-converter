const fs = require('fs');
const path = require('path');
function base64(pathname){ return fs.readFileSync(pathname).toString('base64'); }
const buildDir = process.argv[2];
const template = fs.readFileSync(process.argv[3],'utf8');
const files = fs.readdirSync(buildDir);
// Heuristics: find .js, .data, .wasm
const js = files.find(f=>f.endsWith('.js'));
const data = files.find(f=>f.endsWith('.data'));
const wasm = files.find(f=>f.endsWith('.wasm'));
let out = template.replace('%%BUILD_JS_BASE64%%', base64(path.join(buildDir,js)||''))
                .replace('%%BUILD_DATA_BASE64%%', base64(path.join(buildDir,data)||''))
                .replace('%%BUILD_WASM_BASE64%%', base64(path.join(buildDir,wasm)||''));
fs.writeFileSync(process.argv[4]||'out.html', out);
console.log('Wrote out file');