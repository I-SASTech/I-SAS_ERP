<template>
    <div>
        <multiselect v-model="DisplayValue"  
                     :options="options" 
                     :placeholder="$t('DisplayNameDropdown.DisplayName')" 
                     :searchable="false" 
                     :multiple="false" 
                     track-by="name"  
                     :show-labels="false" 
                     label="name" >
        </multiselect>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        name: 'SalutationDropdown',
        props: ["modelValue", 'disabled', 'newCustomer', 'isProduct'],

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
                if (this.isProduct) {
                    if (this.newCustomer.code != '' && this.newCustomer.code != null && this.newCustomer.code != undefined) {
                        this.options.push({ name: this.newCustomer.code });
                    }
                    if (this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined) {
                        this.options.push({ name: this.newCustomer.code +' '+ this.newCustomer.englishName });
                    }
                    if (this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                        this.options.push({ name: this.newCustomer.code + ' ' + this.newCustomer.arabicName });
                    }
                    if (this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined && this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                        this.options.push({ name: this.newCustomer.code + ' ' + this.newCustomer.englishName + ' ' + this.newCustomer.arabicName });
                    }
                    if (this.newCustomer.description != '' && this.newCustomer.description != null && this.newCustomer.description != undefined) {
                        this.options.push({ name: this.newCustomer.code + ' ' + this.newCustomer.description });
                    }
                    if (this.newCustomer.description != '' && this.newCustomer.description != null && this.newCustomer.description != undefined && this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined && this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                        this.options.push({ name: this.newCustomer.code + ' ' + this.newCustomer.description + ' ' + this.newCustomer.englishName + ' ' + this.newCustomer.arabicName });
                    }
                }
                else {
                    if (this.newCustomer.prefix != '' && this.newCustomer.prefix != null && this.newCustomer.prefix != undefined) {
                        this.options.push({ name: this.newCustomer.prefix + ' ' + this.newCustomer.englishName });
                    }
                    if (this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined) {
                        this.options.push({ name: this.newCustomer.englishName });
                    }
                    if (this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                        this.options.push({ name: this.newCustomer.arabicName });
                    }
                    if (this.newCustomer.prefix != '' && this.newCustomer.prefix != null && this.newCustomer.prefix != undefined && this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined) {
                        this.options.push({ name: this.newCustomer.prefix + ' ' + this.newCustomer.arabicName });
                    }
                    if (this.newCustomer.companyNameEnglish != '' && this.newCustomer.companyNameEnglish != null && this.newCustomer.companyNameEnglish != undefined) {
                        this.options.push({ name: this.newCustomer.companyNameEnglish });
                    }
                    if (this.newCustomer.companyNameArabic != '' && this.newCustomer.companyNameArabic != null && this.newCustomer.companyNameArabic != undefined) {
                        this.options.push({ name: this.newCustomer.companyNameArabic });
                    }

                    if (this.newCustomer.companyNameEnglish != '' && this.newCustomer.companyNameEnglish != null && this.newCustomer.companyNameEnglish != undefined && this.newCustomer.companyNameArabic != '' && this.newCustomer.companyNameArabic != null && this.newCustomer.companyNameArabic != undefined) {
                        this.options.push({ name: this.newCustomer.companyNameEnglish + '  ' + this.newCustomer.companyNameArabic });
                    }
                    if ((this.newCustomer.englishName != '' && this.newCustomer.englishName != null && this.newCustomer.englishName != undefined) && (this.newCustomer.arabicName != '' && this.newCustomer.arabicName != null && this.newCustomer.arabicName != undefined)) {
                        this.options.push({ name: this.newCustomer.englishName + '  ' + this.newCustomer.arabicName });
                    }
                }
                            
                root.value = root.options.find(function (x) {
                    return x.name == root.modelValue;
                })
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
                    if (value == null) {
                    this.$emit('update:modelValue', '');
                }
                    this.value = value;
                    this.$emit('update:modelValue', value.name);
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