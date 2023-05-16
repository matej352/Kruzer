/** @type {import('next').NextConfig} */

const nextConfig = {
  images: {
    domains: ["https://localhost:7295"],
  },
};
/*module.exports = {
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: "http://localhost:7295/api/:path*", // Replace with your backend domain
      },
    ];
  },
};*/

module.exports = nextConfig;
