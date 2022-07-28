import App from './App.vue'
import Vue from 'vue'
import Vuesax from 'vuesax'
import store from './store/store.js'
import 'vuesax/dist/vuesax.css' //Vuesax styles
import 'material-icons/iconfont/material-icons.css';
import './assets/tailwind.css'

Vue.use(Vuesax);

new Vue({
  store,
  render: h => h(App),
}).$mount('#app')
