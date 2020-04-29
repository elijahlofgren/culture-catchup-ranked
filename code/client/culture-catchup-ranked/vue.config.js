module.exports = {
  publicPath: process.env.NODE_ENV === 'production'
    ? '/dist/'
    : '/dist/',

    outputDir: "../../server/wwwroot/dist",
}