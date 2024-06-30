<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="true" placeholder="Select Bank" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true" >
            <!--<p slot="noResult" class="text-danger"> Oops! No Size found.</p>-->
            <template v-slot:noResult>
            <span  class="btn btn-primary " v-on:click="AddSize('Add')">{{ $t('BankDropdown.AddProductSize') }}</span><br />
       </template>
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        name: 'Bankdropdown',
        props: ["modelValue"],
        mixins: [clickMixin],
        components: {
            Multiselect,
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
                render: 0,
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
                
                this.$https.get('/Accounting/BankList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {


                    if (response.data != null) {

                        response.data.banks.forEach(function (cat) {
                            if (cat.shortName == null || cat.shortName == undefined)
                                cat.shortName = '';
                            if (cat.nameArabic == null || cat.nameArabic == undefined)
                                cat.nameArabic = '';
                            if (cat.bankName == null || cat.bankName == undefined)
                                cat.bankName = '';
                            root.options.push({
                                id: cat.id,
                                accountNumber: cat.accountNumber,
                                shortName: cat.shortName,
                               // name: cat.bankName + " " + cat.shortName + ""
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.bankName != '' && cat.bankName != null) ? cat.code + ' ' + cat.bankName + " " + cat.shortName : cat.code + ' ' + cat.nameArabic + " " + cat.shortName : (cat.nameArabic != '' && cat.nameArabic != null) ? cat.code + ' ' + cat.nameArabic + " " + cat.shortName : cat.code + ' ' + cat.bankName + " " + cat.shortName
                            });
                            root.DisplayValue = root.options;

                        });

                    }
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
                    this.$emit('update:modelValue', value);
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