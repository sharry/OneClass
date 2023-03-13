import React from "@vitejs/plugin-react";
import { defineConfig, loadEnv } from "vite";

export default ({ mode }) => {
  process.env = { ...process.env, ...loadEnv(mode, process.cwd()) };

  return defineConfig({
    plugins: [React()],
    root: "src",
    server: {
      port: process.env.VITE_PORT || 3000,
    },
  });
};
