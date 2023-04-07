"use strict";

import Vue from "vue";
import axios from "axios";
import moment from "moment";
import { reload } from "./../config/global";
import vue from "vue";

const v = new vue();
// axios.defaults.baseURL = process.env.baseURL || process.env.apiUrl || '';
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;
axios.defaults.headers["Content-Type"] = "text/plain";
let config = {
  baseURL: `${process.env.VUE_APP_BASEURL}/api/`,
  timeout: 30 * 1000, // Timeout
  // withCredentials: true, // Check cross-site Access-Control
};
const _axios = axios.create(config);
function qianming(params) {
  return window.paramSign(params);
}

_axios.interceptors.request.use(
  function (config) {
    let obj = {};
    let token;
    let timestamp = parseInt(moment().valueOf() / 1000);
    if (localStorage.getItem("token")) {
      token = localStorage.getItem("token");
      config.data = {
        ...config.data,
        timestamp: timestamp,
        machine_code: "WeChat-SA",
        token,
      };
    } else {
      // reload()
      config.data = {
        ...config.data,
        machine_code: "WeChat-SA",
        timestamp: timestamp,
      };
    }
    obj = qianming(config.data);
    if (config.url.includes("?")) {
      config.url = config.url + `&signature=${obj.signature}`;
    } else {
      config.url = config.url + `?signature=${obj.signature}`;
    }

    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);

_axios.interceptors.response.use(
  function (response) {
    let _this = this;
    // removePending(response.config);
    if (response.data.status == 401) {
      localStorage.removeItem("token");
      reload();
    }
    if (response.status >= 500) {
      vue.$toast(response.data.message);
    }
    return response.data;
  },
  function (error) {
    return Promise.reject(error);
  }
);

Plugin.install = function (Vue, options) {
  Vue.axios = _axios;
  window.axios = _axios;
  Object.defineProperties(Vue.prototype, {
    axios: {
      get() {
        return _axios;
      },
    },
    $axios: {
      get() {
        return _axios;
      },
    },
  });
};

Vue.use(Plugin);
export default Plugin;
