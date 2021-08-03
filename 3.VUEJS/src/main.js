import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(VueAxios, axios)
Vue.config.productionTip = false

export const eventBus = new Vue();
export const eventBus1 = new Vue();


new Vue({
  render: h => h(App),
}).$mount('#app')
