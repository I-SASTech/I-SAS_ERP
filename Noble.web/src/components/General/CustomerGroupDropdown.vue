<template>
    <div>
        <multiselect v-model="DisplayValue" v-if="isDisable == 'true'" disabled :options="options" :searchable="false"
            :multiple="false" :placeholder="$t('Select Customer Group')" track-by="name" :clear-on-select="false"
            :show-labels="false" label="name">

        </multiselect>
        <multiselect v-model="DisplayValue" v-else :options="options" :searchable="false" :multiple="false"
            :placeholder="$t('Select Customer Group')" track-by="name" :clear-on-select="false" :show-labels="false"
            label="name">
        </multiselect>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect';
export default {
    props: ["modelValue", "isDisable", 'isMultiple'],
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
            show: false,
            loading: false,
            type: '',
        }
    },
    methods: {
        EmptyRecord: function () {

            this.DisplayValue = '';

        },


        getData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.options = [];
            this.$https.get('/Contact/CustomerGroupList?isDropDown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    response.data.results.forEach(function (cat) {
                        root.options.push({
                            id: cat.id,
                            name: cat.name == '' ? cat.nameAr : cat.name,
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
                    if (this.isMultiple == true) {
                        this.$emit('update:modelValue', value);
                    }
                    else {
                        this.$emit('update:modelValue', value.id);
                    }

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