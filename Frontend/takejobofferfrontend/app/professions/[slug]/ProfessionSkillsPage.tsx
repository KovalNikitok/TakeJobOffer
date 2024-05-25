"use client"

import { useEffect, useState } from "react";
import { getProfessionSkillsWithNames } from "../../services/professionSkills";
import { ProfessionSkills } from "../../components/ProfessionSkills";
import { getProfessionBySlug } from "@/app/services/professions";
import { Loading } from "@/app/components/Loading";

interface ProfessionSkillsProps {
  slug:  string;
}

export default function ProfessionSkillsClient({ slug }: ProfessionSkillsProps) {
    const [professionSkills, setProfessionSkills] = useState<ProfessionSkillWithName[]>([]);
    const [profession, setProfession] = useState<Profession>();
    const [loading, setLoading] = useState(true);
  
    useEffect(() => {
      const getSkillsByProfessionSlug = async () => {
          const professionWithSlug = await getProfessionBySlug(slug);
          const professionSkills = await getProfessionSkillsWithNames(professionWithSlug.id, true);
          
          setProfession(professionWithSlug);
          setProfessionSkills(professionSkills);
          setLoading(false);
      }
      
      getSkillsByProfessionSlug();
    }, []);
  
    return (
      <div>
        {
          loading ? (
            <Loading></Loading>
          ) : (
            <ProfessionSkills values={professionSkills} />
          )
        }
      </div>
    );
}