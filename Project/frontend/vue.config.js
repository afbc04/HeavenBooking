const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true
})

module.exports = {
  devServer: {
    port: 25001,
    proxy: {
      '/api': {
        target: 'http://localhost:25000',
        changeOrigin: true,
        pathRewrite: { '^/api': '' },
        onProxyReq: (proxyReq, req, res) => {
          console.log(`${req.method} ${req.url}`);
        }
      },
    },
  },
};
