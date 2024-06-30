<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :disabled="disabled" :multiple="false"
            v-bind:placeholder="$t('ProductDropdown.PleaseSelectProduct')" track-by="name" :clear-on-select="false"
            :show-labels="false" label="name" :preselect-first="true" v-bind:class="$i18n.locale == 'en' ? 'text-left ' : 'arabicLanguage '">        
            <template v-slot:noResult>
                <p  class="text-danger"> Oops! No Item found.</p>
            </template>
            <!--<a slot="noResult" class="btn btn-primary " v-on:click="AddColor('Add')" v-if="isValid('CanAddColor')">{{$t('ColorDropdown.AddProductColor')}}</a><br />-->
        </multiselect>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'
export default {
    name: 'colordropdown',
    props: ["modelValue", 'disabled'],
    mixins: [clickMixin],

    components: {
        Multiselect
    },
    data: function () {
        return {
            arabic: '',
            english: '',
            options: [],
            value: '',
            loading: false,
        }
    },
    methods: {
        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Product/GetProductForPromotionAndBundleList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    response.data.results.products.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            name: cat.displayName
                        })
                    })
                }
            }).then(function () {
                root.value = root.options.find(function (x) {
                    return x.id == root.modelValue;
                })
            });
        },
    },
    computed: {
        DisplayValue: {
            get: function () {
                if (this.value != "" || this.value != undefined) {
                    return this.value;
                }
                return this.modelValue;
            },
            set: function (value) {
                this.value = value;
                if (value == null || value == undefined) {
                    this.$emit('update:modelValue', value);

                }
                else {
                    this.$emit('update:modelValue', value.id);
                }
            }
        }
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.getData();
    },
}
</script>